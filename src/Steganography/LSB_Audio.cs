using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideSloth.Steganography
{
    public class Audio_LSB
    {
        public string path;
        //-----WaveHeader-----
        public char[] sGroupID; // RIFF
        public uint dwFileLength; // 文件总长度减8，被RIFF占用
        public char[] sRiffType;// 总是 WAVE

        //-----WaveFormatChunk-----
        public char[] sFChunkID;         // 四个字节: "fmt "
        public uint dwFChunkSize;        // 标头长度（以字节为单位）
        public ushort wFormatTag;       // 1 (MS PCM)
        public ushort wChannels;        // 通道数
        public uint dwSamplesPerSec;    // 频率（赫兹）
        public uint dwAvgBytesPerSec;   // RAM 分布，每秒字节数
        public ushort wBlockAlign;      // 样本帧大小（以字节为单位）
        public ushort wBitsPerSample;    // 样本中的位

        //-----WaveDataChunk-----
        public char[] sDChunkID;     // "data"
        public uint dwDChunkSize;    // 标头长度（以字节为单位）
        public byte dataStartPos;  // 音频开始日期位置

        // wav 文件初始化
        public Audio_LSB()
        {
            path = Environment.CurrentDirectory;
            //-----WaveHeader-----
            dwFileLength = 0;
            sGroupID = "RIFF".ToCharArray();
            sRiffType = "WAVE".ToCharArray();

            //-----WaveFormatChunk-----
            sFChunkID = "fmt ".ToCharArray();
            dwFChunkSize = 16;
            wFormatTag = 1;
            wChannels = 2;
            dwSamplesPerSec = 44100;
            wBitsPerSample = 16;
            wBlockAlign = (ushort)(wChannels * (wBitsPerSample / 8));
            dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;

            //-----WaveDataChunk-----
            dataStartPos = 44;
            dwDChunkSize = 0;
            sDChunkID = "data".ToCharArray();
        }

        // wav文件录制方法（只需复制)
        public  void WriteWav(string oldpath, string path)
        {
            FileStream fsr = new FileStream(oldpath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fsr);
            FileStream fsw;
            try
            {
                fsw = new FileStream(path, FileMode.CreateNew);
            }
            catch (IOException)
            {
                fsw = new FileStream(path, FileMode.Truncate);
            }
            BinaryWriter w = new BinaryWriter(fsw);
            int pos = 0, len = (int)r.BaseStream.Length; short temp;
            while (pos < len)
            {
                temp = (short)r.ReadInt16();
                w.Write(temp);
                pos += 2;
            }
            r.Close(); w.Close();
            fsr.Close(); fsw.Close();
        }

        // 从流写入新文件并在其中写入消息的方法
        public static void Encode_Audio(string oldpath, string path, byte[] bufferMessage)
        {
            byte DataPos = 44;
            byte[] source;
            using (BinaryReader b = new BinaryReader(File.Open(oldpath, FileMode.Open)))
            {
                int length = (int)b.BaseStream.Length;
                source = b.ReadBytes(length);
            }
            int sourceLength = bufferMessage.Length * 4; // 我们消息的长度
            byte[] sourcelen = BitConverter.GetBytes(sourceLength);
            int offlen = DataPos + 1;
            // 我们在前 16 个字节中加密消息的长度，在每 2 个最低有效位中加密 wav 文件的日期部分
            foreach (byte x in sourcelen)
            {
                int multy = 192;
                for (int i = 6; i >= 0; i = i - 2)
                {
                    int output = (x & multy) >> i;
                    multy = multy / 4;
                    int temp = source[offlen] & 252;
                    source[offlen] = (byte)(temp | output);
                    offlen++;
                }
            }

            int offset = offlen;
            try
            {
                if (source.Length < bufferMessage.Length * 4 + DataPos + 16)
                {
                    throw new Exception("消息长度大于文件长度");
                }

                // 将消息的两位写入源字节的 2 个最低有效位
                foreach (byte x in bufferMessage)
                {
                    int multiply = 192;
                    for (int i = 6; i >= 0; i = i - 2)
                    {
                        int output = (x & multiply) >> i;
                        multiply = multiply / 4;
                        int temp = source[offset] & 252;
                        source[offset] = (byte)(temp | output);
                        offset++;
                    }
                }
                using (BinaryWriter b = new BinaryWriter(File.Open(path, FileMode.Create)))
                {
                    foreach (byte i in source)
                    {
                        b.Write(i);
                    }
                }
            }
            catch 
            {
                throw;
            }
        }
        // 消息解码器
        public static byte[] Decode_Audio(string path)
        {
            byte DataPos = 44;
            byte[] source;
            using (BinaryReader b = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                int length = (int)b.BaseStream.Length;
                source = b.ReadBytes(length);
            }
            byte[] bufferlen = new byte[4];
            int lenstep = 0;
            int offlen = 6;
            // 提取我们消息的长度
            for (int i = DataPos + 1; i < DataPos + 17; i = i + 4)
            {
                offlen = 6;
                int multy = 192;
                int output = 0;
                for (int k = 0; k < 4; k++)
                {
                    int temp = source[i + k];
                    temp = (temp << offlen) & multy;
                    output = output | temp;
                    multy = multy / 4;
                    offlen = offlen - 2;
                }
                bufferlen[lenstep] = (byte)(output);
                lenstep++;
            }
            int bufferLength = BitConverter.ToInt32(bufferlen, 0);
            byte[] bufferOutput = new byte[bufferLength / 4];
            //从二进制流中提取消息
            int step = 0;
            int offset;
            for (int i = DataPos + 17; i < DataPos + 17 + bufferLength; i = i + 4)
            {
                offset = 6;
                int multiply = 192;
                int output = 0;
                for (int k = 0; k < 4; k++)
                {
                    int temp = source[i + k];
                    temp = (temp << offset) & multiply;
                    output = output | temp;
                    multiply = multiply / 4;
                    offset = offset - 2;
                }
                bufferOutput[step] = (byte)(output);
                step++;
            }
            return bufferOutput;
        }

        public string Analyze(Audio_LSB file2, string path1, string path2)
        {
            byte DataPos1 = this.dataStartPos;
            byte DataPos2 = file2.dataStartPos;
            byte[] source1, source2;
            using (BinaryReader b1 = new BinaryReader(File.Open(path1, FileMode.Open)))
            {
                int length1 = (int)b1.BaseStream.Length;
                source1 = b1.ReadBytes(length1);
            }
            using (BinaryReader b2 = new BinaryReader(File.Open(path2, FileMode.Open)))
            {
                int length2 = (int)b2.BaseStream.Length;
                source2 = b2.ReadBytes(length2);
            }
            int arrlen = source1.Length - (DataPos1 + 1);
            int[] NumberArray = new int[arrlen];
            for (int i = 0; i < arrlen; i++)
            {
                NumberArray[i] = 0;
            }
            string str = "文件中修改的数据字节数:";
            int step = 0;
            for (int i = DataPos2 + 1; i < source2.Length; i++)
            {
                if (source2[i] != source1[i])
                {
                    NumberArray[step] = i;
                    step++;
                }
            }
            for (int i = 0; i < arrlen; i++)
            {
                if (NumberArray[i] != 0)
                {
                    str += "  " + Convert.ToString(NumberArray[i]);
                }
            }
            if (str == "文件中修改的数据字节数:")
                str = "文件的音频数据部分没有变化.";
            return str;
        }
    }

}
