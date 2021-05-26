using System;
using System.IO;
using System.Windows;

namespace TI_Lab_4
{
    class LFSR
    { 
        private static ulong registerValue { get; set; }
        private static ulong keyValue { get; set; }
        public static string filePath { get; set; }

        private const int bufferLength = 8192;
        private const int charBit = 8;

        public static void SetKey(ulong key)
        {
            registerValue = key;
            keyValue = key;
        }

        public static void SetFilePath(string path)
        {
            filePath = path;
        }

       public static void Encrypt()
        {            
            byte[] buffer = new byte[bufferLength];
            int size;

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    size = reader.Read(buffer, 0, bufferLength);

                    while (size != 0)
                    {
                        for (int i = 0; i < bufferLength; i++)
                        {
                            for (int j = 0; j < charBit; j++)
                            {
                                buffer[i] = (byte)(buffer[i] ^ (Fibonacci() << (charBit - j)));
                            }
                        }

                        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath + ".enc", FileMode.Create)))
                        {
                            writer.Write(buffer, 0, size);
                        }

                        size = reader.Read(buffer, 0, bufferLength);
                    }
                }
                
                MessageBox.Show("Done", "Info", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Decrypt()
        {
            byte[] buffer = new byte[bufferLength];
            int size;

            registerValue = keyValue;

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    string newFilePath = filePath.Substring(0, filePath.LastIndexOf(".enc"));
                    size = reader.Read(buffer, 0, bufferLength);
                    while (size != 0)
                    {
                        for (int i = 0; i < bufferLength; i++)
                        {
                            for (int j = 0; j < charBit; j++)
                            {
                                buffer[i] = (byte)(buffer[i] ^ (Fibonacci() << (charBit - j)));
                            }
                        }

                        using (BinaryWriter writer = new BinaryWriter(File.Open(newFilePath, FileMode.Create)))
                        {
                            writer.Write(buffer, 0, size);
                        }

                        size = reader.Read(buffer, 0, bufferLength);
                    }
                }
                MessageBox.Show("Done", "Info", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private static ulong Fibonacci() 
        {                       
            registerValue = ((((registerValue >> 0) ^ (registerValue >> 1) ^ (registerValue >> 3) ^
                             (registerValue >> 4) ^ (registerValue >> 24)) & 1) << 31) | (registerValue >> 1);                              
            return registerValue & 1;
        }
    }
}
