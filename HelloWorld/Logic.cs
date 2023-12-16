using HideSloth.Crypto;
using HideSloth.Steganography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HideSloth.MainForm;
using System.Windows.Forms;
using System.Diagnostics;

namespace HideSloth
{
    public class ProgressEventArgs : EventArgs
    {
        public int Progress { get; set; }
        public string Message { get; set; }


        public ProgressEventArgs(int progress, string message)
        {
            Progress = progress;
            Message = message;
        }
    }

    public class Logic
    {
        //public delegate void ProgressReportHandler(string message);
        //public event ProgressReportHandler ProgressReported;
        public event EventHandler<ProgressEventArgs>? ProgressChanged;
        public event EventHandler<FileSaveRequestEventArgs>? RequestFileSave;
        public event EventHandler<RouteOutputRequestEventArgs>? RequestRouteSave;
        public event EventHandler<SaveExtractedFileEventArgs>? RequestExtractedSave;
        protected virtual void OnProgressChanged(ProgressEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
        public async Task CallMethodAsync(bool ismult, string selecte_secret, bool audiochecked,string password)
        {
            await Task.Run(() => LongRunningOperation(ismult, selecte_secret, audiochecked, password));
        }


        /*  something useless
                                   Action updateAction = () =>
                            {
                                form1.log = "!";
                                
                            };

                            // 触发事件
                            form1.BeginInvoke((Action)(() =>
                            {
                                form1.TriggerControlAction(updateAction);
                            }));
        */
        public bool LongRunningOperation(bool ismult, string selecte_secret, bool audiochecked,string password)
        {
            string manyfilePath = "";

            if (GlobalVariables.mode == "Normal")
            {

                if (GlobalVariables.encode && GlobalVariables.enableencrypt && GlobalVariables.isfile)//Encode encrypted file
                {
                    try
                    {
                        if (ismult)
                        {
                            var argss = new RouteOutputRequestEventArgs();
                            RequestRouteSave?.Invoke(this, argss);
                            manyfilePath = argss.WaitForPath();
                        }

                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            byte[] secretData = BytesStringThings.ReadFileToByteswithName(selecte_secret);

                            OnProgressChanged(new ProgressEventArgs(0, "Secret File Readed"));

                            DateTime lastaccess = new DateTime(2021, 8, 15);

                            secretData = Aes_ChaCha_Encryptor.Encrypt(secretData, password, out byte[] salt, out byte[] nonce, out byte[] tag);
                            OnProgressChanged(new ProgressEventArgs(0, "Secret File Encrypted and stored in memory"));

                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            string newroutename = "";

                            if (!audiochecked)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap? result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {
                                    OnProgressChanged(new ProgressEventArgs(0, "Start to embed"));

                                    result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, secretData)), loaded);

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    OnProgressChanged(new ProgressEventArgs(0, "Start to embed"));

                                    result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));
                                }

                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();

                                    result?.Save(filePath, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    loaded.Dispose();
                                    result?.Dispose();
                                }
                                if (ismult == true && manyfilePath != null)
                                {
                                    if (GlobalVariables.keepformat)
                                    {                                       
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                        result?.Dispose();
                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                        result?.Dispose();
                                    }
                                }


                            }
                            else if (audiochecked)
                            {
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();

                                    OnProgressChanged(new ProgressEventArgs(0, "Start to embed"));

                                    Audio_LSB.Encode_Audio(single_container, filePath, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));

                                }
                                if (ismult && manyfilePath != null)
                                {
                                    newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));
                                }
                            }


                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));
                            }

                            OnProgressChanged(new ProgressEventArgs(1, "Success to encode file with encryption to image"));

                        }
                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));

                    }

                }


                if (GlobalVariables.decode && GlobalVariables.enableencrypt && GlobalVariables.isfile)//Decode encrypted file
                {

                    byte[]? extracted_result = new byte[0];
                    try
                    {
                        if (!audiochecked)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);

                            if (GlobalVariables.Algor == "LSB")
                            {
                                extracted_result = Convert.FromBase64String(LSB_Image.extract(unloading));
                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                extracted_result = Core_Linear_Image.DecodeFileFromImage(unloading);
                            }
                            unloading.Dispose();

                        }
                        else if (audiochecked)
                        {
                            extracted_result = Audio_LSB.Decode_Audio(GlobalVariables.route_container);

                        }
                        OnProgressChanged(new ProgressEventArgs(0, "Container Readed"));
                        extracted_result = Aes_ChaCha_Decryptor.Decrypt(extracted_result, password);
                        OnProgressChanged(new ProgressEventArgs(0, "Decrypted file Successful"));

                        int nameserperatorindex = BytesStringThings.FindSeparatorIndex(extracted_result, GlobalVariables.separator);
                        GlobalVariables.defaultname = BytesStringThings.ExtractFileName(extracted_result, nameserperatorindex);

                        var args = new SaveExtractedFileEventArgs();
                        RequestExtractedSave?.Invoke(this, args);
                        var filePath = args.WaitForPath();

                        extracted_result = BytesStringThings.ExtractFileContent(extracted_result, nameserperatorindex);

                        if (filePath != null)
                        {
                            BytesStringThings.BytesWritetoFile(filePath, extracted_result);
                            extracted_result = null;
                        }
                        OnProgressChanged(new ProgressEventArgs(1, "Success to decode file with encryption from image"));

                    }

                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }

                }


                if (GlobalVariables.encode && GlobalVariables.disablencrypt && GlobalVariables.isfile)//Encode plain file
                {
                    try
                    {
                        if (ismult)
                        {
                            var argss = new RouteOutputRequestEventArgs();
                            RequestRouteSave?.Invoke(this, argss);
                            manyfilePath = argss.WaitForPath();
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            string newroutename = "";
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            if (!audiochecked)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                OnProgressChanged(new ProgressEventArgs(0, "Loaded container in memory Successful, start to embed"));

                                Bitmap? result = null;
                                if (GlobalVariables.Algor == "LSB")
                                {
                                    

                                    string StringFromFile = BytesStringThings.ReadFileToStringwithName(selecte_secret);
                                    OnProgressChanged(new ProgressEventArgs(0, "Readed secret File in memory Successful"));

                                    result = LSB_Image.embed(StringFromFile, loaded);
                                    OnProgressChanged(new ProgressEventArgs(0, "Embed File in memory Successful, select a route to save"));

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.ReadFileToByteswithName(selecte_secret));
                                }
                                OnProgressChanged(new ProgressEventArgs(0, "Readed secret File and embed in container in memory Successful, please save"));

                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();
                                    result?.Save(filePath, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    loaded.Dispose();

                                }
                                if (manyfilePath != null && ismult)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                    }
                                }
                                OnProgressChanged(new ProgressEventArgs(1, "Success to encode file without encryption to image"));


                            }
                            else if (audiochecked)
                            {
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();

                                    OnProgressChanged(new ProgressEventArgs(0, "Start to embed"));
                                    Audio_LSB.Encode_Audio(single_container, filePath, BytesStringThings.ReadFileToByteswithName(selecte_secret));

                                }
                                if (manyfilePath != null && ismult)
                                {
                                    newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.ReadFileToByteswithName(selecte_secret));
                                }
                                OnProgressChanged(new ProgressEventArgs(1, "Saved Successful"));
                            }


                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }


                if (GlobalVariables.decode && GlobalVariables.disablencrypt && GlobalVariables.isfile)//Decode plain file
                {
                    try
                    {
                        byte[]? data = new byte[0];
                        if (!audiochecked)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            OnProgressChanged(new ProgressEventArgs(0, "Readed loaded container"));


                            if (GlobalVariables.Algor == "LSB")
                            {
                                data = Convert.FromBase64String(LSB_Image.extract(unloading));
                                OnProgressChanged(new ProgressEventArgs(0, "Extracted File Successful"));

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                data = Core_Linear_Image.DecodeFileFromImage(unloading);
                                OnProgressChanged(new ProgressEventArgs(0, "Extracted File Successful"));
                            }
                            unloading.Dispose();
                        }
                        else if (audiochecked)
                        {
                            data = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            OnProgressChanged(new ProgressEventArgs(0, "Extracted File Successful"));
                        }
                        int nameserperatorindex = BytesStringThings.FindSeparatorIndex(data, GlobalVariables.separator);
                        GlobalVariables.defaultname = BytesStringThings.ExtractFileName(data, nameserperatorindex);

                        var args = new SaveExtractedFileEventArgs();
                        RequestExtractedSave?.Invoke(this, args);
                        var filePath = args.WaitForPath();

                        data = BytesStringThings.ExtractFileContent(data, nameserperatorindex);

                        if (filePath != null)
                        {
                            BytesStringThings.BytesWritetoFile(filePath, data);
                            data = null;
                        }
                        OnProgressChanged(new ProgressEventArgs(1, "Success to decode file with encryption from image"));


                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }

                }

                //Start string
                if (GlobalVariables.encode && GlobalVariables.enableencrypt && GlobalVariables.isstring)//Encode encrypted string
                {
                    try
                    {
                        if (ismult)
                        {
                            var argss = new RouteOutputRequestEventArgs();
                            RequestRouteSave?.Invoke(this, argss);
                            manyfilePath = argss.WaitForPath();
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }

                            string newroutename = "";
                            byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo));

                            byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, password, out byte[] salt, out byte[] nonce, out byte[]  tag);
                            OnProgressChanged(new ProgressEventArgs(0, "Encrypted String Successful"));
                            if (!audiochecked)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                OnProgressChanged(new ProgressEventArgs(0, "Container Loaded Successful, start to embed"));

                                Bitmap? result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {

                                    result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                    OnProgressChanged(new ProgressEventArgs(0, "Embed string Successful, please save loaded container"));

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {

                                    result = Core_Linear_Image.EncodeMsgLinearImage(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                    OnProgressChanged(new ProgressEventArgs(0, "Embed string Successful, please save loaded container"));
                                }
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();
                                    result?.Save(filePath, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                }
                                if (manyfilePath != null && ismult == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    }
                                }
                                OnProgressChanged(new ProgressEventArgs(1, "Saved Successful"));
                                loaded.Dispose();
                                result?.Dispose();
                            }
                            else if (audiochecked)
                            {
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();
                                    Audio_LSB.Encode_Audio(single_container, filePath, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));

                                }
                                if (manyfilePath != null && ismult)
                                {
                                    newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));

                                }
                            }
                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }

                        }
                        //form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to encode string with encryption to image", "Success")));

                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }


                if (GlobalVariables.decode && GlobalVariables.enableencrypt && GlobalVariables.isstring)//Decode encrypted string
                {

                    try
                    {
                        if (!audiochecked)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            OnProgressChanged(new ProgressEventArgs(0, "Readed Loaded container Successful"));

                            string encrypted_result = "";
                            string rawresult = "";
                            string result = "";

                            if (GlobalVariables.Algor == "LSB")
                            {
                                encrypted_result = LSB_Image.extract(unloading);
                                OnProgressChanged(new ProgressEventArgs(0, "Extracted String Successful"));

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                rawresult = Core_Linear_Image.DecodeMsgLinearImage(unloading);
                                OnProgressChanged(new ProgressEventArgs(0, "Extracted String Successful"));

                                int nullCharIndex = rawresult.IndexOf('\0');
                                if (nullCharIndex != -1)
                                {
                                    encrypted_result = rawresult.Substring(0, nullCharIndex);
                                }

                            }
                            result = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), password));
                            unloading.Dispose();
                            OnProgressChanged(new ProgressEventArgs(3, result));
                        }
                        else if (audiochecked)
                        {
                            string result_audio = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Audio_LSB.Decode_Audio(GlobalVariables.route_container), password));
                            OnProgressChanged(new ProgressEventArgs(3, result_audio));

                        }

                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }

                }


                if (GlobalVariables.encode && GlobalVariables.disablencrypt && GlobalVariables.isstring)//Encode plain string
                {

                    try
                    {
                        if (ismult)
                        {
                            var argss = new RouteOutputRequestEventArgs();
                            RequestRouteSave?.Invoke(this, argss);
                            manyfilePath = argss.WaitForPath();
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            string newroutename = "";

                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            if (!audiochecked)
                            {

                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap? result = null;
                                OnProgressChanged(new ProgressEventArgs(0, "Loaded container Successful"));

                                if (GlobalVariables.Algor == "LSB")
                                {
                                    result = LSB_Image.embed(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo), loaded);
                                    OnProgressChanged(new ProgressEventArgs(0, "Embed String Successful"));
                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    result = Core_Linear_Image.EncodeMsgLinearImage(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo), loaded);
                                    OnProgressChanged(new ProgressEventArgs(0, "Embed String Successful"));
                                }
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();
                                    result?.Save(filePath, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                }
                                if (manyfilePath != null && ismult == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(manyfilePath, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result?.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    }
                                }
                                OnProgressChanged(new ProgressEventArgs(1, "Saved Successful"));
                                loaded.Dispose();
                                result?.Dispose();
                            }
                            else if (audiochecked)
                            {
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    RequestFileSave?.Invoke(this, args);
                                    var filePath = args.WaitForPath();
                                    Audio_LSB.Encode_Audio(single_container, filePath, Encoding.UTF8.GetBytes(GlobalVariables.stringinfo));

                                }
                                if (manyfilePath != null && ismult)
                                {
                                    newroutename = Path.Combine(manyfilePath, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, Encoding.UTF8.GetBytes(GlobalVariables.stringinfo));

                                }
                            }

                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));
                            }

                        }
                        //form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to encode plain text to image", "Success")));

                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }


                if (GlobalVariables.decode && GlobalVariables.disablencrypt && GlobalVariables.isstring)//Decode plain string
                {

                    string result = "";
                    string rawresult = "";
                    try
                    {
                        if (!audiochecked)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);

                            OnProgressChanged(new ProgressEventArgs(0, "Readed Loaded container Successful"));

                            if (GlobalVariables.Algor == "LSB")
                            {
                                result = LSB_Image.extract(unloading);

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                rawresult = Core_Linear_Image.DecodeMsgLinearImage(unloading);

                                int nullCharIndex = rawresult.IndexOf('\0');
                                if (nullCharIndex != -1)
                                {
                                    result = rawresult.Substring(0, nullCharIndex);
                                }
                            }
                            OnProgressChanged(new ProgressEventArgs(3, System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(result))));
                            unloading.Dispose();
                        }
                        else if (audiochecked)
                        {
                            string result_audio = System.Text.Encoding.UTF8.GetString(Audio_LSB.Decode_Audio(GlobalVariables.route_container));
                            OnProgressChanged(new ProgressEventArgs(3, result_audio));
                        }

                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }


                if ((GlobalVariables.encode == false && GlobalVariables.decode == false) || (GlobalVariables.enableencrypt == false && GlobalVariables.disablencrypt == false) )
                {
                    OnProgressChanged(new ProgressEventArgs(2, "Lack of Choice(s)"));
                }

            }
            else if (GlobalVariables.mode == "Encryptor")
            {

                if (GlobalVariables.encode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    //form1.Invoke(new Action(() => form1.Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileEnc.EncryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, password);
                        }
                        //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted File And Saved Successfully" + "\n", false)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                }
                else if (GlobalVariables.decode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    //form1.Invoke(new Action(() => form1.Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileEnc.DecryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, password);
                        }
                        //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Decrypted File And Saved Successfully" + "\n", false)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }

                }

                else if (GlobalVariables.encode && GlobalVariables.isstring)
                {
                    byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo));

                    byte[] salt, nonce, tag;
                    //         
                    byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, password, out salt, out nonce, out tag);
                    //form1.Invoke(new Action(() => form1.outputstr = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)))));
                    //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted String Successfully" + "\n", false)));
                    // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                }

                else if (GlobalVariables.decode && GlobalVariables.isstring)
                {
                    try
                    {
                        string result = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(GlobalVariables.stringinfo), password));
                        //form1.Invoke(new Action(() => form1.outputstr = result));
                        //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Decrypted String Successfully" + "\n", false)));
                        // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        //form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        //form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));

                    }
                }


            }

            GlobalVariables.stringinfo = "";

            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            return true;
        }
    }
    public class FileSaveRequestEventArgs : EventArgs
    {
        private TaskCompletionSource<string> _pathCompletionSource = new TaskCompletionSource<string>();

        public void SetPath(string path)
        {
            _pathCompletionSource.SetResult(path);
        }

        public string WaitForPath()
        {
            return _pathCompletionSource.Task.Result; // 同步等待路径
        }
    }

    public class RouteOutputRequestEventArgs: EventArgs
    {
        private TaskCompletionSource<string> _pathCompletionSource = new TaskCompletionSource<string>();

        public void SetPath(string path)
        {
            _pathCompletionSource.SetResult(path);
        }

        public string WaitForPath()
        {
            return _pathCompletionSource.Task.Result; // 同步等待路径
        }

    }

    public class SaveExtractedFileEventArgs: EventArgs
    {
        private TaskCompletionSource<string> _pathCompletionSource = new TaskCompletionSource<string>();

        public void SetPath(string path)
        {
            _pathCompletionSource.SetResult(path);
        }

        public string WaitForPath()
        {
            return _pathCompletionSource.Task.Result; // 同步等待路径
        }

    }


}
