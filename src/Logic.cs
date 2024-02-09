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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;
using static HideSloth.GlobalVariables;

namespace HideSloth
{
    public class ProgressEventArgs(int progress, string message) : EventArgs
    {
        public int Progress { get; set; } = progress;
        public string Message { get; set; } = message;
    }

    public class Logic
    {
        private readonly SimpleEventAggregator ProgressChanged = SimpleEventAggregator.Instance;
        //public event EventHandler<ProgressEventArgs>? ProgressChanged;

        //public event EventHandler<FileSaveRequestEventArgs>? RequestFileSave;
        //public event EventHandler<RouteOutputRequestEventArgs>? RequestRouteSave;
        //public event EventHandler<SaveExtractedFileEventArgs>? RequestExtractedSave;
        protected virtual void OnProgressChanged(ProgressEventArgs e)
        {
            ProgressChanged.Publish(e);
        }
        public async Task CallMethodAsync(bool ismult, string selecte_secret, bool audiochecked,string password,string stringinfo, List<string> Containers, bool encode, bool decode, bool isfile, bool isstring)
        {
            await Task.Run(() => LongRunningOperation(ismult, selecte_secret, audiochecked, password, stringinfo,Containers, encode, decode, isfile, isstring));
        }

        public bool LongRunningOperation(bool ismult, string selecte_secret, bool audiochecked,string password,string stringinfo, List<string> Containers, bool encode, bool decode, bool isfile, bool isstring)
        {
            string multipalPath = "";
            string currentName = "";
            DateTime lastaccess = new DateTime(2021, 8, 15);

            if (GlobalVariables.Mode == "Normal")
            {
                if (encode)
                {
                    try
                    {
                        if (ismult)//if it is mulitpla Encode, choose output route first
                        {
                            var argss = new RouteOutputRequestEventArgs();
                            ProgressChanged.Publish(argss);
                            multipalPath = argss.WaitForPath();
                        }
                        byte[] secretData = new byte[0];
                        foreach (string single_container in Containers)
                        {
                            if (isfile)
                            {
                                secretData = BytesStringThings.ReadFileToByteswithName(selecte_secret);
                            }
                            else if (isstring)
                            {
                                secretData = Convert.FromBase64String(BytesStringThings.StringtoBase64(stringinfo));
                            }
                            OnProgressChanged(new ProgressEventArgs(0, "Secret information Readed"));

                            if (GlobalVariables.Enableencrypt)
                            {
                                secretData = Aes_ChaCha_Encryptor.Encrypt(secretData, password, out byte[] salt, out byte[] nonce, out byte[] tag);
                                secretData = BytesStringThings.CombineBytes(salt, nonce, tag, secretData);
                                OnProgressChanged(new ProgressEventArgs(0, "Secret Information Encrypted and stored in memory"));
                            }

                            if (GlobalVariables.Copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }

                            if (!audiochecked)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap? result = null;
                                OnProgressChanged(new ProgressEventArgs(0, "Start to Encode"));

                                var stegoAlg = AlgorithmImageFactory.CreateAlgorithm(GlobalVariables.Algor);
                                result = stegoAlg.Encode(loaded, secretData,password);

                                if (ismult == false)//single file process request name now
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    ProgressChanged.Publish(args);
                                    currentName = args.WaitForPath();

                                    result?.Save(currentName, Support_Converter.SaveFormatImage(GlobalVariables.Outputformat));
                                }
                                if (ismult == true && multipalPath != null)
                                {
                                    if (GlobalVariables.Keepformat)
                                    {
                                        currentName = Path.Combine(multipalPath, (Path.GetFileName(single_container)));
                                        result?.Save(currentName, Support_Converter.SaveFormatImage(GlobalVariables.Outputformat));
                                    }
                                    else
                                    {
                                        currentName = Path.Combine(multipalPath, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.Outputformat);
                                        result?.Save(currentName, Support_Converter.SaveFormatImage(GlobalVariables.Outputformat));
                                    }
                                }
                                loaded.Dispose();
                                result?.Dispose();

                            }
                            else if (audiochecked)
                            {
                                if (ismult == false)
                                {
                                    var args = new FileSaveRequestEventArgs();
                                    ProgressChanged.Publish(args);
                                    currentName = args.WaitForPath();

                                    OnProgressChanged(new ProgressEventArgs(0, "Start to Encode"));

                                    Audio_LSB.Encode_Audio(single_container, currentName, secretData);

                                }
                                if (ismult && multipalPath != null)
                                {
                                    currentName = Path.Combine(multipalPath, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, currentName, secretData);
                                }
                            }

                            if (GlobalVariables.Copymeta)
                            {
                                File.SetCreationTime(currentName, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(currentName, lastaccess);
                                File.SetLastWriteTime(currentName, File.GetLastWriteTime(single_container));
                            }
                            OnProgressChanged(new ProgressEventArgs(1, "Secret Information Embeded successfully!"));

                        }
                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }
                else if (decode)
                {
                    try
                    {

                        byte[]? extracted_result = new byte[0];
                        if (!audiochecked)
                        {
                            Bitmap unloading = new Bitmap(Containers[0]);

                            var stegoAlg = AlgorithmImageFactory.CreateAlgorithm(GlobalVariables.Algor);
                            extracted_result = stegoAlg.Decode(unloading,password);
                            unloading.Dispose();

                        }
                        else if (audiochecked)
                        {
                            extracted_result = Audio_LSB.Decode_Audio(Containers[0]);

                        }
                        OnProgressChanged(new ProgressEventArgs(0, "Container Readed"));
                        if (GlobalVariables.Enableencrypt)
                        {
                            extracted_result = Aes_ChaCha_Decryptor.Decrypt(extracted_result, password);
                            OnProgressChanged(new ProgressEventArgs(0, "Decrypted file Successful"));
                        }
                        if (isfile)
                        {
                            int nameserperatorindex = BytesStringThings.FindSeparatorIndex(extracted_result, GlobalVariables.Separator);
                            GlobalVariables.Defaultname = BytesStringThings.ExtractFileName(extracted_result, nameserperatorindex);
                            var args = new SaveExtractedFileEventArgs();
                            ProgressChanged.Publish(args);
                            var filePath = args.WaitForPath();
                            extracted_result = BytesStringThings.ExtractFileContent(extracted_result, nameserperatorindex);
                            if (filePath != null)
                            {
                                BytesStringThings.BytesWritetoFile(filePath, extracted_result);
                                extracted_result = null;
                            }
                            OnProgressChanged(new ProgressEventArgs(1, "Success to decode file with encryption from image"));
                        }
                        if (isstring)
                        {
                            string extracted_string = extracted_result != null ? System.Text.Encoding.UTF8.GetString(extracted_result) : "null_value";
                            OnProgressChanged(new ProgressEventArgs(3, extracted_string));

                        }
                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }
            }


            else if (GlobalVariables.Mode == "Encryptor")
            {

                if (encode && isfile)
                {
                    GlobalVariables.Defaultname = "";
                    var args = new SaveExtractedFileEventArgs();
                    ProgressChanged.Publish(args);
                    var filePath = args.WaitForPath();
                    try
                    {
                        if (filePath != null)
                        {
                            FileEnc.EncryptFile(selecte_secret, filePath, password);
                        }
                        OnProgressChanged(new ProgressEventArgs(1, "AS ONLY ENCRYPTOR: Encrypted File And Saved Successfully"));

                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }
                else if (decode && isfile)
                {
                    GlobalVariables.Defaultname = "";
                    var args = new SaveExtractedFileEventArgs();
                    ProgressChanged.Publish(args);
                    var filePath = args.WaitForPath();
                    try
                    {
                        if (filePath != null)
                        {
                            FileEnc.DecryptFile(selecte_secret, filePath, password);
                        }
                        OnProgressChanged(new ProgressEventArgs(1, "AS ONLY ENCRYPTOR: Decrypted File And Saved Successfully"));
                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }

                }

                else if (encode && isstring)
                {
                    byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(stringinfo));
                    byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, password, out byte[] salt, out byte[] nonce, out byte[] tag);
                    string output = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)));
                    OnProgressChanged(new ProgressEventArgs(3, output));

                }

                else if (decode && isstring)
                {
                    try
                    {
                        string output = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(stringinfo), password));
                        OnProgressChanged(new ProgressEventArgs(3, output));
                    }
                    catch (Exception ex)
                    {
                        OnProgressChanged(new ProgressEventArgs(2, ex.Message));
                    }
                }

            }

            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            return true;
        }
        public static string CheckCapacity(List<string> Containers, string secretfile, bool check_multi)
        {
            string outputcapacity = "";

            var stegoAlg = AlgorithmImageFactory.CreateAlgorithm(GlobalVariables.Algor);
            List<double> imagesizes = new List<double>();
            foreach (string single in Containers)
            {
                Image img = Image.FromFile(single);
                imagesizes.Add(stegoAlg.CheckSize(img));
                img.Dispose();
            }
            if (check_multi && Containers != null)
            {
                outputcapacity = "The smallest container's capacity is: " + (imagesizes.Min().ToString()) + " KB;";
            }
            else if (check_multi != true && Containers != null)
            {
                outputcapacity = "The container's capacity is: " + (imagesizes.Min().ToString()) + " KB;";
            }
            imagesizes.Clear();

            bool secretSlah = secretfile.Contains(@"\");
            if (secretSlah == true)
            {
                FileInfo fileInfo = new FileInfo(secretfile);

                // 获取文件大小
                long fileSizeInBytes = fileInfo.Length;
                outputcapacity += " The secret file's size is " + fileSizeInBytes / 1024 + " KB.";
            }
            return outputcapacity;
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
