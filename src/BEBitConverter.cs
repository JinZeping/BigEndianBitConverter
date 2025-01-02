using System;

namespace BigEndianBitConverter
{
    /// <summary>
    /// 采用大端模式的字节转换器
    /// </summary>
    public static class BEBitConverter
    {
        /// <summary>
        /// 大端模式，高位字节在前
        /// </summary>
        public readonly static bool IsLittleEndian = false;

        public static ushort ToUInt16(byte[] buffer, int startIndex = 0)
        {
            return (ushort)((buffer[startIndex] << 8) + buffer[startIndex + 1]);
        }

        public static short ToInt16(byte[] buffer, int startIndex = 0)
        {
            return (short)((buffer[startIndex] << 8) + buffer[startIndex + 1]);
        }

        public static uint ToUInt32(byte[] buffer, int startIndex = 0)
        {
            return (uint)((buffer[startIndex] << 24)
                | (buffer[startIndex + 1] << 16)
                | (buffer[startIndex + 2] << 8)
                | buffer[startIndex + 3]);
        }

        public static int ToInt32(byte[] buffer, int startIndex = 0)
        {
            return (buffer[startIndex] << 24)
                | (buffer[startIndex + 1] << 16)
                | (buffer[startIndex + 2] << 8)
                | buffer[startIndex + 3];
        }

        public static float ToSingle(byte[] buffer, int startIndex = 0)
        {
            byte[] bytes = new byte[4];
            Array.Copy(buffer, startIndex, bytes, 0, 4);

            if (BitConverter.IsLittleEndian != IsLittleEndian)
            {
                Array.Reverse(buffer, startIndex, 4);
            }

            float f = BitConverter.ToSingle(bytes, 0);

            return f;
        }

        public static double ToDouble(byte[] buffer, int startIndex = 0)
        {
            byte[] bytes = new byte[8];
            Array.Copy(buffer, startIndex, bytes, 0, 8);

            if (BitConverter.IsLittleEndian != IsLittleEndian)
            {
                Array.Reverse(buffer, startIndex, 8);
            }

            double d = BitConverter.ToDouble(bytes, 0);

            return d;
        }

        public static byte[] GetBytes(ushort us)
        {
            return new byte[]
            {
                (byte)(us >> 8),
                (byte)us
            };
        }

        public static byte[] GetBytes(short s)
        {
            return new byte[]
            {
                (byte)(s >> 8),
                (byte)s
            };
        }

        public static byte[] GetBytes(uint ui)
        {
            return new byte[]
            {
                (byte)(ui >> 24),
                (byte)(ui >> 16),
                (byte)(ui >> 8),
                (byte)ui
            };
        }

        public static byte[] GetBytes(int i)
        {
            return new byte[]
            {
                (byte)(i >> 24),
                (byte)(i >> 16),
                (byte)(i >> 8),
                (byte)i
            };
        }

        public static byte[] GetBytes(uint ui, int length)
        {
            byte[] bytes = GetBytes(ui);

            if (bytes[0] > 0)
            {
                throw new Exception("UInt24 memory overflow");
            }

            byte[] result = new byte[length];
            Array.Copy(bytes, 4 - length, result, 0, length);
            return result;
        }

        public static byte[] GetBytes(int i, int length)
        {
            byte[] bytes = GetBytes(i);

            if (bytes[0] > 0 && bytes[0] < 255)
            {
                throw new Exception("Int24 memory overflow");
            }

            byte[] result = new byte[length];
            Array.Copy(bytes, 4 - length, result, 0, length);
            return result;
        }

        public static byte[] GetBytes(float f)
        {
            byte[] bytes = BitConverter.GetBytes(f);

            if (BitConverter.IsLittleEndian != IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        public static byte[] GetBytes(double d)
        {
            byte[] bytes = BitConverter.GetBytes(d);

            if (BitConverter.IsLittleEndian != IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }
    }
}
