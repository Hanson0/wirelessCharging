using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassMyLoad
{
    class MyLoad
    {
        public static byte[] SetIRequest(byte loadAddr,float floatI)
        {
            //组合负载仪固定部分
            byte[] frameSetI = new byte[11];//LoadAddr
            byte[] fixedI = { loadAddr, 0x10, 0x0A, 0x01, 0x00, 0x02 };
            Buffer.BlockCopy(fixedI, 0, frameSetI, 0, fixedI.Length);

            //计算 Ilen 、 HexItemI、CRC

            //计算每一项电流值对应的16进制;300mA0.3
            byte[] HexItemI = FloatToHex(floatI / 1000.0f);
            //获取2.3转换成Hex后有几个字节
            byte[] frameIlen = BitConverter.GetBytes(HexItemI.Length);
            byte Ilen = frameIlen[0];
            Buffer.BlockCopy(frameIlen, 0, frameSetI, 6, 1);
            Buffer.BlockCopy(HexItemI, 0, frameSetI, 7, HexItemI.Length);
            //重合后计算CRC
            byte[] bytesCrc = CRC16(frameSetI, true);
            byte[] bytesSetI = new byte[13];
            //重合前面(除CRC)给新byte[]
            Buffer.BlockCopy(frameSetI, 0, bytesSetI, 0, frameSetI.Length);
            //重合CRC
            Buffer.BlockCopy(bytesCrc, 0, bytesSetI, 11, bytesCrc.Length);

            return bytesSetI;
        }

        private static byte[] FloatToHex(float f)
        {
            byte[] b = BitConverter.GetBytes(f);
            Array.Reverse(b);
            return b;
        }

        private static byte[] CRC16(byte[] data, bool isReverse)
        {
            int len = data.Length;
            if (len > 0)
            {
                ushort crc = 0xFFFF;

                for (int i = 0; i < len; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
                    }
                }
                byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
                byte lo = (byte)(crc & 0x00FF);         //低位置
                if (isReverse)
                {
                    return new byte[] { lo, hi };//高8位在前，为[0]
                }
                return new byte[] { hi, lo };//高8位在前，为[0]
            }
            return new byte[] { 0, 0 };
        }

    }
}
