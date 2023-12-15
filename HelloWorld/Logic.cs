using HideSloth.Crypto;
using HideSloth.Steganography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HideSloth.MainForm;
using System.Windows.Forms;

namespace HideSloth
{
    public class Logic
    {
        //public delegate void ProgressReportHandler(string message);
        //public event ProgressReportHandler ProgressReported;
        private MainForm form1;

        public Logic(MainForm form1)
        {
            this.form1 = form1;
        }
        public async Task CallMethodAsync()
        {
            await Task.Run(() => LongRunningOperation());
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
        public bool LongRunningOperation()
        {

            if (GlobalVariables.mode == "Normal")
            {

                if (GlobalVariables.encode && GlobalVariables.enableencrypt && GlobalVariables.isfile)//Encode encrypted file
                {

                    try
                    {
                        if (form1.is_Mult)
                        {

                            form1.Invoke(new Action(() => form1.mulitpla_output()));//for duplicate copy, choose route first
                        }
                        //ShowMessageOnUIThread(GlobalVariables.route_containers[0], "");

                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            byte[] secretData = BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret);


                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Secret File Readed\n")));

                           DateTime lastaccess = new DateTime(2021, 8, 15);

                            secretData = Aes_ChaCha_Encryptor.Encrypt(secretData, GlobalVariables.password, out byte[] salt, out byte[] nonce, out byte[] tag);
                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Secret File Encrypted and stored in memory\n")));
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            string newroutename = "";

                            if (GlobalVariables.audioorimage == "image")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);

                                Bitmap result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Start to embed\n")));
                                   result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, secretData)), loaded);

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Start to embed\n")));

                                    result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));
                                }

                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && form1.is_Mult == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    loaded.Dispose();
                                    result.Dispose();
                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                        result.Dispose();
                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                        result.Dispose();
                                    }
                                }


                            }
                            else if (GlobalVariables.audioorimage == "audio")
                            {
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_wav()));
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Start to embed\n")));

                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));

                                }
                            }


                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }
                            form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Loaded container saved\n", false)));
                            form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to encode file with encryption to image", "Success")));


                        }
                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));
                        form1.Invoke(new Action(() => form1.BoldToLog(ex.Message, true)));
                    }

                }


                if (GlobalVariables.decode && GlobalVariables.enableencrypt && GlobalVariables.isfile)//Decode encrypted file
                {

                    byte[]? extracted_result = new byte[0];
                    byte[]? decrypted_content = new byte[0];

                    try
                    {
                        if (GlobalVariables.audioorimage == "image")
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
                        else if (GlobalVariables.audioorimage == "audio")
                        {
                            //byte[] xx = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            extracted_result = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false)));
                            // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        }
                        form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Container Readed\n")));

                        extracted_result = Aes_ChaCha_Decryptor.Decrypt(extracted_result, GlobalVariables.password);
                        //extracted_result = null;

                        int nameserperatorindex = BytesStringThings.FindSeparatorIndex(extracted_result, GlobalVariables.separator);
                        BytesStringThings.ExtractFileName(extracted_result, nameserperatorindex);
                        form1.Invoke(new Action(() => form1.Outputfile_any()));

                        extracted_result = BytesStringThings.ExtractFileContent(extracted_result, nameserperatorindex);
                        //extracted_result = null;

                        if (GlobalVariables.outputnameandroute != null)
                        {
                            BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, extracted_result);
                            extracted_result = null;
                        }
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Extracted and decrypted file.\n", false)));

                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to decode file with encryption from image", "Success")));


                    }

                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));
                    }
                    finally
                    {


                    }

                }


                if (GlobalVariables.encode && GlobalVariables.disablencrypt && GlobalVariables.isfile)//Encode plain file
                {
                    try
                    {
                        if (form1.is_Mult)
                        {

                            form1.Invoke(new Action(() => form1.mulitpla_output()));//for duplicate copy, choose route first
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            string newroutename = "";
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            if (GlobalVariables.audioorimage == "image")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap result = null;
                                if (GlobalVariables.Algor == "LSB")
                                {
                                    

                                    string StringFromFile = BytesStringThings.ReadFileToStringwithName(GlobalVariables.route_secret);
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Readed secret File in memory Successful\n")));

                                    result = LSB_Image.embed(StringFromFile, loaded);
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Embed File in memory Successful, select a route to save\n")));
                                    //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Loaded container in memory Successful, start to embed.\n")));
                                    //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                    result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));
                                }

                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Readed secret File and embed in container in memory Successful, please save\n")));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                //
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && form1.is_Mult == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    loaded.Dispose();

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                    }
                                }

                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Saved Successful\n")));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());


                            }
                            else if (GlobalVariables.audioorimage == "audio")
                            {
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));

                                }
                            }


                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }

                        }
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to encode file without encryption to image", "Success")));

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));
                    }
                }


                if (GlobalVariables.decode && GlobalVariables.disablencrypt && GlobalVariables.isfile)//Decode plain file
                {
                    try
                    {
                        byte[] data = new byte[0];
                        if (GlobalVariables.audioorimage == "image")
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);

                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Readed loaded container\n")));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            if (GlobalVariables.Algor == "LSB")
                            {
                                data = Convert.FromBase64String(LSB_Image.extract(unloading));
                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Extracted File Successful\n")));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                data = Core_Linear_Image.DecodeFileFromImage(unloading);
                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Extracted File Successful\n")));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            }
                            unloading.Dispose();
                        }
                        else if (GlobalVariables.audioorimage == "audio")
                        {
                            data = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Extracted File Successful\n")));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        }
                        int nameserperatorindex = BytesStringThings.FindSeparatorIndex(data, GlobalVariables.separator);
                        BytesStringThings.ExtractFileName(data, nameserperatorindex);
                        form1.Invoke(new Action(() => form1.Outputfile_any()));

                        data = BytesStringThings.ExtractFileContent(data, nameserperatorindex);
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, data);
                        }
                        form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Saved Successful\n")));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success decode file without encryption from image", "Success")));



                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));
                    }

                }

                //Start string
                if (GlobalVariables.encode && GlobalVariables.enableencrypt && GlobalVariables.isstring)//Encode encrypted string
                {
                    try
                    {
                        if (form1.is_Mult)
                        {

                            form1.Invoke(new Action(() => form1.mulitpla_output()));//for duplicate copy, choose route first
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

                            byte[] salt, nonce, tag;
                            //         
                            byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, GlobalVariables.password, out salt, out nonce, out tag);
                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Encrypted String Successful\n")));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            if (GlobalVariables.audioorimage == "image")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Container Loaded Successful, start to embed\n")));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                Bitmap result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {

                                    result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Embed string Successful, please save loaded container\n")));
                                    //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {

                                    result = Core_Linear_Image.EncodeMsgLinearImage(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Embed string Successful, please save loaded container\n")));
                                    // UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                }
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && form1.is_Mult == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    }
                                }
                                form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false)));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();
                            }
                            else if (GlobalVariables.audioorimage == "audio")
                            {
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
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
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to encode string with encryption to image", "Success")));

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));

                    }
                }


                if (GlobalVariables.decode && GlobalVariables.enableencrypt && GlobalVariables.isstring)//Decode encrypted string
                {

                    try
                    {
                        if (GlobalVariables.audioorimage == "image")
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Readed Loaded container Successful\n")));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            string encrypted_result = "";
                            string rawresult = "";
                            string result = "";

                            if (GlobalVariables.Algor == "LSB")
                            {
                                encrypted_result = LSB_Image.extract(unloading);
                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Extracted String Successful\n")));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                rawresult = Core_Linear_Image.DecodeMsgLinearImage(unloading);
                                form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Extracted String Successful\n")));
                                // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                int nullCharIndex = rawresult.IndexOf('\0');
                                if (nullCharIndex != -1)
                                {
                                    encrypted_result = rawresult.Substring(0, nullCharIndex);
                                }

                            }
                            result = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), GlobalVariables.password));
                            form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false)));
                            // UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            unloading.Dispose();
                            form1.Invoke(new Action(() => form1.outputstr = result));
                            form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to decode string with encryption from image", "Success")));
                        }
                        else if (GlobalVariables.audioorimage == "audio")
                        {
                            //byte[] xx = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            string result_audio = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Audio_LSB.Decode_Audio(GlobalVariables.route_container), GlobalVariables.password));
                            form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false)));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            form1.Invoke(new Action(() => form1.outputstr = result_audio));
                            form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to decode string with encryption from wav audio", "Success")));

                        }

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));

                    }

                }


                if (GlobalVariables.encode && GlobalVariables.disablencrypt && GlobalVariables.isstring)//Encode plain string
                {




                    try
                    {
                        if (form1.is_Mult)
                        {

                            form1.Invoke(new Action(() => form1.mulitpla_output()));//for duplicate copy, choose route first
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            string newroutename = "";

                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            if (GlobalVariables.audioorimage == "image")
                            {

                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Loaded container Successful\n")));
                                    // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                    result = LSB_Image.embed(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo), loaded);
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Embed String Successful\n")));
                                    // UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Loaded container Successful\n")));
                                    // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                    result = Core_Linear_Image.EncodeMsgLinearImage(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo), loaded);
                                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Embed String Successful\n")));
                                    //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                }
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && form1.is_Mult == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    }
                                }
                                form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false)));
                                //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();
                            }
                            else if (GlobalVariables.audioorimage == "audio")
                            {
                                if (form1.is_Mult == false)
                                {
                                    form1.Invoke(new Action(() => form1.Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, Encoding.UTF8.GetBytes(GlobalVariables.stringinfo));

                                }
                                if (GlobalVariables.multipal_route != null && form1.is_Mult)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
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
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to encode plain text to image", "Success")));

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        // UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));


                    }
                }


                if (GlobalVariables.decode && GlobalVariables.disablencrypt && GlobalVariables.isstring)//Decode plain string
                {


                    string result = "";
                    string rawresult = "";
                    try
                    {
                        if (GlobalVariables.audioorimage == "image")
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Readed Loaded container Successful\n")));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            if (GlobalVariables.Algor == "LSB")
                            {
                                result = LSB_Image.extract(unloading);
                                form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Extracted String Successful\n", false)));
                                // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                rawresult = Core_Linear_Image.DecodeMsgLinearImage(unloading);
                                form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Extracted String Successful\n", false)));
                                // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                int nullCharIndex = rawresult.IndexOf('\0');
                                if (nullCharIndex != -1)
                                {
                                    result = rawresult.Substring(0, nullCharIndex);
                                }
                            }
                            form1.Invoke(new Action(() => form1.outputstr = BytesStringThings.Base64toString(result)));
                            form1.Invoke(new Action(() => form1.outputstr = BytesStringThings.Base64toString(result)));
                            unloading.Dispose();
                            form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to deocode plain text from image", "Success")));
                        }
                        else if (GlobalVariables.audioorimage == "audio")
                        {
                            //byte[] xx = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            string result_audio = System.Text.Encoding.UTF8.GetString(Audio_LSB.Decode_Audio(GlobalVariables.route_container));
                            form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false)));
                            //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            form1.Invoke(new Action(() => form1.outputstr = result_audio));
                            form1.Invoke(new Action(() => form1.outputstr = result_audio));
                            form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Success to decode string with encryption from wav audio", "Success")));

                        }

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));

                    }
                    finally
                    {

                    }
                }


                if ((GlobalVariables.encode == false && GlobalVariables.decode == false) || (GlobalVariables.enableencrypt == false && GlobalVariables.disablencrypt == false) )
                {
                    form1.Invoke(new Action(() => form1.log = (DateTime.Now.ToString() + "--- Lack of Choice\n")));
                    //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                    form1.Invoke(new Action(() => form1.ShowMessageOnUIThread("Lack of one or more Choices", "Error")));

                }


                GC.Collect();

            }
            else if (GlobalVariables.mode == "Encryptor")
            {

                if (GlobalVariables.encode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    form1.Invoke(new Action(() => form1.Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileEnc.EncryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, GlobalVariables.password);
                        }
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted File And Saved Successfully" + "\n", false)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                }
                else if (GlobalVariables.decode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    form1.Invoke(new Action(() => form1.Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileEnc.DecryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, GlobalVariables.password);
                        }
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Decrypted File And Saved Successfully" + "\n", false)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }

                }

                else if (GlobalVariables.encode && GlobalVariables.isstring)
                {
                    byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(GlobalVariables.stringinfo));

                    byte[] salt, nonce, tag;
                    //         
                    byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, GlobalVariables.password, out salt, out nonce, out tag);
                    form1.Invoke(new Action(() => form1.outputstr = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)))));
                    form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted String Successfully" + "\n", false)));
                    // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                }

                else if (GlobalVariables.decode && GlobalVariables.isstring)
                {
                    try
                    {
                        string result = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(GlobalVariables.stringinfo), GlobalVariables.password));
                        form1.Invoke(new Action(() => form1.outputstr = result));
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Decrypted String Successfully" + "\n", false)));
                        // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        form1.Invoke(new Action(() => form1.BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true)));
                        //UpdateUI(() => richTextBoxLog.ScrollToCaret());
                        form1.Invoke(new Action(() => form1.ShowMessageOnUIThread(ex.Message, "Error")));

                    }
                }


            }

            GlobalVariables.stringinfo = "";

            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            return true;
        }

    }
}
