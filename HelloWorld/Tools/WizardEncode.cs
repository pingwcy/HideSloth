using HideSloth.Crypto;
using HideSloth.Steganography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HideSloth.Tools
{
    public class WizardEncode
    {
        public List<string> otherfileslists;

        public static void GetFilePaths(string directory, List<string> filePaths, int depth)
        {
            if (depth == 0)
            {
                return;
            }

            try
            {
                // 获取当前目录下的所有文件
                string[] files = Directory.GetFiles(directory);
                filePaths.AddRange(files);

                // 获取所有子目录
                string[] subDirs = Directory.GetDirectories(directory);
                foreach (string dir in subDirs)
                {
                    // 递归调用
                    GetFilePaths(dir, filePaths, depth - 1);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("没有权限访问目录: " + directory);
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("目录不存在: " + directory);
                Console.WriteLine(e.Message);
            }
        }




        /*
        public static void GetDirectories(string sourceDir, List<string> directories, int depth)
        {
            if (depth == 0)
            {
                return;
            }

            try
            {
                // 添加当前目录
                directories.Add(sourceDir);

                // 获取所有子目录
                string[] subDirs = Directory.GetDirectories(sourceDir);
                foreach (string dir in subDirs)
                {
                    // 递归调用
                    GetDirectories(dir, directories, depth - 1);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("没有权限访问目录: " + sourceDir);
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("目录不存在: " + sourceDir);
                Console.WriteLine(e.Message);
            }
        }
        */
        public static void CopyDirectoryStructure(string sourceDir, string destinationDir, List<string> createdDirectories, int maxDepth, int currentDepth = 0)
        {
            if (currentDepth > maxDepth)
            {
                return;
            }

            // 获取所有子目录
            foreach (string dirPath in Directory.GetDirectories(sourceDir))
            {
                // 计算目标目录路径
                string destDirPath = dirPath.Replace(sourceDir, destinationDir);

                // 创建目标目录
                Directory.CreateDirectory(destDirPath);

                // 添加到列表中
                createdDirectories.Add(destDirPath);

                // 递归复制子目录
                CopyDirectoryStructure(dirPath, destDirPath, createdDirectories, maxDepth, currentDepth + 1);
            }
        }


        public static bool StegoLarge(string pwd, List<int> bufflist, string containers_route, string route_secret, string output_route, List<string> container_list, List<string> otherfilelist, List<string> ALLfilePaths, List<string> smallimagelist, Action<string> updateStatus, CancellationToken token)
        {
            string individualroutecontainer = "";
            List<string> realrouteofcontainers = [];
            //Judge do we need  copy the dir structrure
            if (Form_EncodeWizard.keepstrcuture == true)
            {
                foreach (string singlecandidate in ALLfilePaths)
                {
                    individualroutecontainer = RemoveFirstFolderFromPath(GetRelativePath(containers_route, singlecandidate));
                    if (Path.GetDirectoryName(individualroutecontainer) != "")
                    {
                        string aa = (output_route + @"\\" + Path.GetDirectoryName(individualroutecontainer));
                        Directory.CreateDirectory(aa);
                        if (GlobalVariables.copyotherfilemeta)
                        {
                            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(Path.GetDirectoryName(individualroutecontainer));
                            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(aa);

                            DateTime creationTime = sourceDirectoryInfo.CreationTime;
                            targetDirectoryInfo.CreationTime = creationTime;

                            DateTime lastWriteTime = sourceDirectoryInfo.LastWriteTime;
                            targetDirectoryInfo.LastWriteTime = lastWriteTime;

                            DateTime lastAccessTime = sourceDirectoryInfo.LastAccessTime;
                            targetDirectoryInfo.LastAccessTime = lastAccessTime;

                        }
                    }
                }

            }
            FileInfo fileInfo = new FileInfo(route_secret);
            long fileSiz = fileInfo.Length;


            int cycle = 0;
            using (FileStream fs = new FileStream(route_secret, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                byte[] buffer;
                long currentpostion = 0;
                while (true)
                {

                    // 读取指定数量的字节
                    try
                    {
                        token.ThrowIfCancellationRequested();

                    }
                    catch
                    {

                        return false;
                    }
                    if (fs.Position >= fs.Length)
                    {
                        // 已到达文件末尾
                        break;
                    }

                    buffer = reader.ReadBytes(Convert.ToInt32(bufflist[cycle]));
                    /*
                    if (buffer.Length == 0)
                    {
                        // 如果没有更多数据，跳出循环
                        break;
                    }
                    */
                    
                    if (IsImageFile(Path.Combine(containers_route, container_list[cycle])))
                    {

                        try
                        {
                            //cycle number
                            long buf = buffer.Length;
                        byte[] intBytes = BitConverter.GetBytes(currentpostion);
                            currentpostion += buf;
                        //if (BitConverter.IsLittleEndian)
                        //Array.Reverse(intBytes0); 确保字节顺序正确
                        //byte[] intBytes = intBytes0.Concat(GlobalVariables.separator).ToArray();


                        byte[] stringBytes = BitConverter.GetBytes(fileSiz);
                        //byte[] stringBytes = stringBytes0.Concat(GlobalVariables.separator).ToArray();


                        byte[] fullbuffer = new byte[intBytes.Length + stringBytes.Length + buffer.Length];


                        Buffer.BlockCopy(intBytes, 0, fullbuffer, 0, intBytes.Length);
                        Buffer.BlockCopy(stringBytes, 0, fullbuffer, intBytes.Length, stringBytes.Length);
                        Buffer.BlockCopy(buffer, 0, fullbuffer, intBytes.Length + stringBytes.Length, buffer.Length);


                        if (GlobalVariables.enableencrypt)
                        {
                                fullbuffer = Aes_ChaCha_Encryptor.Encrypt(fullbuffer, pwd, out byte[] salt, out byte[] nonce, out byte[] tag);
                                fullbuffer = BytesStringThings.CombineBytes(salt, nonce, tag, fullbuffer);
                         }
                        Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(Path.Combine(containers_route, container_list[cycle]));
                        if (GlobalVariables.Algor == "LSB")
                        {
                            Bitmap result = LSB_Image.embed(Convert.ToBase64String(fullbuffer), loaded);
                            string dirrela = "";
                            string onlyname = "";
                            if (Path.GetDirectoryName(container_list[cycle]) != "")
                            {
                                dirrela = Path.GetDirectoryName(container_list[cycle]) + @"\\";

                            }
                            if (GlobalVariables.keepformat)
                            {
                                onlyname = Path.GetFileName(container_list[cycle]);
                            }
                            else
                            {
                                onlyname = Path.GetFileNameWithoutExtension(container_list[cycle]) + GlobalVariables.outputformat;

                            }

                            result.Save((output_route + @"\\" + dirrela + onlyname), Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                            loaded.Dispose();

                            result.Dispose();
                            updateStatus?.Invoke($"Saved: {Path.Combine(output_route, container_list[cycle])}" + ",Number: " + (cycle + 1).ToString() + ".");
                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime((output_route + @"\\" + dirrela + onlyname), File.GetCreationTime(Path.Combine(containers_route, container_list[cycle])));
                                File.SetLastAccessTime((output_route + @"\\" + dirrela + onlyname), File.GetLastAccessTime(Path.Combine(containers_route, container_list[cycle])));
                                File.SetLastWriteTime((output_route + @"\\" + dirrela + onlyname), File.GetLastWriteTime(Path.Combine(containers_route, container_list[cycle])));

                            }

                        }
                        else if (GlobalVariables.Algor == "Linear")
                        {
                            Bitmap result = Core_Linear_Image.EncodeFileLinear(loaded, fullbuffer);

                            string dirrela = "";
                            string onlyname = "";
                            if (Path.GetDirectoryName(container_list[cycle]) != "")
                            {
                                dirrela = Path.GetDirectoryName(container_list[cycle]) + @"\\";

                            }
                            if (GlobalVariables.keepformat)
                            {
                                onlyname = Path.GetFileName(container_list[cycle]);
                            }
                            else
                            {
                                onlyname = Path.GetFileNameWithoutExtension(container_list[cycle]) + GlobalVariables.outputformat;

                            }


                            result.Save((output_route + @"\\" + dirrela + onlyname), Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                            loaded.Dispose();

                            result.Dispose();
                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime((output_route + @"\\" + dirrela + onlyname), File.GetCreationTime(Path.Combine(containers_route, container_list[cycle])));
                                File.SetLastAccessTime((output_route + @"\\" + dirrela + onlyname), File.GetLastAccessTime(Path.Combine(containers_route, container_list[cycle])));
                                File.SetLastWriteTime((output_route + @"\\" + dirrela + onlyname), File.GetLastWriteTime(Path.Combine(containers_route, container_list[cycle])));

                            }
                            updateStatus?.Invoke($"Saved: {Path.Combine(output_route, container_list[cycle])}" + ",Number: " + (cycle + 1).ToString() + ".");
                        }

                        }
                        
                        catch (Exception ex)
                        {
                            return false;
                        }
                        
                        //

                        // 如果读取的数据小于缓冲区大小，表示已到达文件末尾
                        /*
                        if (buffer.Length < bufflist[cycle])
                        {
                            break;
                        }
                        */
                        if (buffer.Length < bufflist[cycle] || fs.Position >= fs.Length)
                        {
                            break;
                        }


                        cycle++;

                    }


                }
                //copy non image files
                updateStatus?.Invoke($"Now we will start copy non image file, please wait");

                if (Form_EncodeWizard.copynonimage == true && Form_EncodeWizard.keepstrcuture == false)
                {
                    foreach (string filePath in otherfilelist)
                    {
                        if (IsImageFile(filePath) != true)
                        {
                            // 从文件路径中提取文件名
                            string fileName = Path.GetFileName(filePath);

                            // 构造目标文件的完整路径
                            string destFile = Path.Combine(output_route, fileName);
                            // updateStatus?.Invoke(fileName+"    "+destFile);

                            // 复制文件
                            File.Copy(filePath, destFile, overwrite: true);
                            if (GlobalVariables.copyotherfilemeta)
                            {
                                File.SetCreationTime(Path.GetFullPath(destFile), File.GetCreationTime(filePath));
                                File.SetLastAccessTime(Path.GetFullPath(destFile), File.GetLastAccessTime(filePath));
                                File.SetLastWriteTime(Path.GetFullPath(destFile), File.GetLastWriteTime(filePath));

                            }

                            updateStatus?.Invoke($"Copied: " + filePath + "\n");

                        }
                    }
                   // updateStatus?.Invoke($"Copy Finnished");

                }
                else if (Form_EncodeWizard.copynonimage == true && Form_EncodeWizard.keepstrcuture == true)
                {
                    foreach (string filePath in ALLfilePaths)
                    {
                        if (IsImageFile(filePath) != true)
                        {
                            // 从文件路径中提取文件名

                            string relativeroute = RemoveFirstFolderFromPath(GetRelativePath(containers_route, filePath));
                            if (relativeroute != "")
                            {
                                relativeroute = @"\\" + relativeroute;
                            }
                            string fileoutputroute = output_route + relativeroute;

                            // 构造目标文件的完整路径
                            //string destFile = Path.Combine(output_route, fileName);
                            // updateStatus?.Invoke(fileName+"    "+destFile);

                            // 复制文件
                            File.Copy(filePath, Path.GetFullPath(fileoutputroute), overwrite: false);
                            if (GlobalVariables.copyotherfilemeta)
                            {
                                File.SetCreationTime(Path.GetFullPath(fileoutputroute), File.GetCreationTime(filePath));
                                File.SetLastAccessTime(Path.GetFullPath(fileoutputroute), File.GetLastAccessTime(filePath));
                                File.SetLastWriteTime(Path.GetFullPath(fileoutputroute), File.GetLastWriteTime(filePath));

                            }
                            updateStatus?.Invoke($"Copied: " + filePath + "\n");

                        }
                    }


                }
                //here is some small images we can not use
                if (smallimagelist.Count != 0)
                {
                    foreach (string smallroute in smallimagelist)
                    {
                        string filePath = Path.GetFullPath(containers_route +@"\\"+ smallroute);
                        string relativeroute = RemoveFirstFolderFromPath(GetRelativePath(containers_route, filePath));
                        if (relativeroute != "")
                        {
                            relativeroute = @"\\" + relativeroute;
                        }
                        string fileoutputroute = output_route + relativeroute;

                        // 构造目标文件的完整路径
                        //string destFile = Path.Combine(output_route, fileName);
                        // updateStatus?.Invoke(fileName+"    "+destFile);

                        // 复制文件
                        File.Copy(filePath, Path.GetFullPath(fileoutputroute), overwrite: false);
                        if (GlobalVariables.copyotherfilemeta)
                        {
                            File.SetCreationTime(Path.GetFullPath(fileoutputroute), File.GetCreationTime(filePath));
                            File.SetLastAccessTime(Path.GetFullPath(fileoutputroute), File.GetLastAccessTime(filePath));
                            File.SetLastWriteTime(Path.GetFullPath(fileoutputroute), File.GetLastWriteTime(filePath));

                        }
                        updateStatus?.Invoke($"Copied: " + filePath + "\n");

                    }
                }
                updateStatus?.Invoke($"Copy Finnished");
                GC.Collect(2, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

                return true;
            }
        }

        public static bool Encryptor(string pwd, string routeofsecrets, string routeofoutput, Action<string> updateStatus)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(routeofsecrets);
            FileInfo[] files = directoryInfo.GetFiles();

            foreach (string file in Directory.GetFiles(routeofsecrets))
            {
                FileEnc.EncryptFile(file, Path.Combine(routeofoutput, Path.GetFileName(file)), pwd);
                updateStatus?.Invoke($"Encrypted: {file}");
            }
            updateStatus?.Invoke("All files encrypted and saved!");

            return true;
        }

        public static List<string> otherfiles(string folderPath, List<string> ALLfilePaths)
        {
            List<string> otherfilelist = new List<string>();

            if (Form_EncodeWizard.searchdeep)
            {
                foreach (string fileName in ALLfilePaths)
                {
                    if (IsImageFile(fileName) != true)
                    {
                        otherfilelist.Add(fileName);
                    }
                }

            }
            else
            {
                string[] fileEntries = Directory.GetFiles(folderPath).OrderBy(path => Path.GetFileName(path)).ToArray();
                foreach (string fileName in fileEntries)
                {
                    if (IsImageFile(fileName) != true)
                    {
                        otherfilelist.Add(fileName);
                    }
                }
            }
            return otherfilelist;

        }
        static string RemoveFirstFolderFromPath(string path)
        {
            int indexOfFirstBackslash = path.IndexOf('\\');
            if (indexOfFirstBackslash != -1)
            {
                return path.Substring(indexOfFirstBackslash + 1);
            }
            return path;
        }

        public static string GetRelativePath(string basePath, string absolutePath)
        {
            var baseUri = new Uri(basePath);
            var absoluteUri = new Uri(absolutePath);

            if (!baseUri.IsAbsoluteUri || !absoluteUri.IsAbsoluteUri)
            {
                throw new InvalidOperationException("路径必须是绝对路径");
            }

            var relativeUri = baseUri.MakeRelativeUri(absoluteUri);

            // Uri 的 ToString 方法会将路径分隔符转换为正斜杠（/），可能需要将它们转换回反斜杠（\）
            return Uri.UnescapeDataString(relativeUri.ToString()).Replace('/', '\\');
        }

        public static List<ImageInfo> CheckCapacity(string folderPath, List<string> Allfile, out List<string> smallimages)
        {
            List<ImageInfo> list = new List<ImageInfo>();
            smallimages = [];
            if (Form_EncodeWizard.searchdeep)
            {
                foreach (string fileName in Allfile)
                {
                    if (IsImageFile(fileName))
                    {
                        using (Image img = Image.FromFile(fileName))
                        {
                            string dimensions = "";
                            if (GlobalVariables.Algor == "Linear")
                            {
                                dimensions = Math.Round(img.Width * img.Height / 1024 * 0.97).ToString() + " KB";

                            }
                            if (GlobalVariables.Algor == "LSB")
                            {
                                dimensions = Math.Round(img.Width * img.Height * 3 / 8 * 0.89 / 1024 / 1.34).ToString() + " KB";
                                //MessageBox.Show("No code!!!!");

                            }
                            if (Int32.Parse(dimensions.Substring(0, dimensions.Length - 3)) <= GlobalVariables.smallstandard)
                            {
                                smallimages.Add(RemoveFirstFolderFromPath(GetRelativePath(folderPath, fileName)));
                            }
                            else
                            {
                                list.Add(new ImageInfo
                                {
                                    FileName = RemoveFirstFolderFromPath(GetRelativePath(folderPath, fileName)),
                                    Dimensions = dimensions
                                });
                            }
                        }
                    }
                }

            }
            else
            {
                string[] fileEntries = Directory.GetFiles(folderPath).OrderBy(path => Path.GetFileName(path)).ToArray();
                foreach (string fileName in fileEntries)
                {
                    if (IsImageFile(fileName))
                    {
                        using (Image img = Image.FromFile(fileName))
                        {
                            string dimensions = "";
                            if (GlobalVariables.Algor == "Linear")
                            {
                                dimensions = Math.Round(img.Width * img.Height / 1024 * 0.97).ToString() + " KB";

                            }
                            if (GlobalVariables.Algor == "LSB")
                            {
                                dimensions = Math.Round(img.Width * img.Height * 3 / 8 * 0.89 / 1024 / 1.34).ToString() + " KB";
                                //MessageBox.Show("No code!!!!");

                            }

                            if (Int32.Parse(dimensions.Substring(0, dimensions.Length - 3)) <= 1)
                            {
                                smallimages.Add(RemoveFirstFolderFromPath(GetRelativePath(folderPath, fileName)));
                            }
                            else
                            {
                                list.Add(new ImageInfo
                                {
                                    FileName = RemoveFirstFolderFromPath(GetRelativePath(folderPath, fileName)),
                                    Dimensions = dimensions
                                });
                            }
                        }
                    }
                }
            }
            return list;
        }
        public static bool IsImageFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".png" || extension == ".bmp" || extension == ".jpeg" || extension == ".tiff" || extension == ".gif";
        }
        public class ImageInfo
        {
            public string? FileName { get; set; }
            public string? Dimensions { get; set; }
        }

    }
}
