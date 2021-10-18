using System.Runtime.InteropServices;
using System.Text;

// if system encoding and target encoding not matched, reverse it
public static class BinaryManager
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct IntegerHolder
    {
        [FieldOffset(0)]
        public readonly int Int32;

        [FieldOffset(0)]
        public readonly uint UInt32;

        [FieldOffset(0)]
        public readonly byte byte0;
        [FieldOffset(1)]
        public readonly byte byte1;
        [FieldOffset(2)]
        public readonly byte byte2;
        [FieldOffset(3)]
        public readonly byte byte3;

        // encoding order is based on system
        public IntegerHolder(int value)
        {
            this = default;
            this.Int32 = value;
        }

        public IntegerHolder(uint value)
        {
            this = default;
            this.UInt32 = value;
        }

        // encoding order is based on stream
        public IntegerHolder(ref byte[] bytes, int offset, bool isLittleEndian)
        {
            this = default;

            if (System.BitConverter.IsLittleEndian != isLittleEndian) {
                this.byte0 = bytes[offset + 3];
                this.byte1 = bytes[offset + 2];
                this.byte2 = bytes[offset + 1];
                this.byte3 = bytes[offset];
            }
            else {
                this.byte0 = bytes[offset];
                this.byte1 = bytes[offset + 1];
                this.byte2 = bytes[offset + 2];
                this.byte3 = bytes[offset + 3];
            }
        }

        public void Write(ref byte[] bytes, int offset, bool isLittleEndian)
        {
            if (System.BitConverter.IsLittleEndian != isLittleEndian) {
                bytes[offset + 0] = byte3;
                bytes[offset + 1] = byte2;
                bytes[offset + 2] = byte1;
                bytes[offset + 3] = byte0;
            }
            else {
                bytes[offset + 0] = byte0;
                bytes[offset + 1] = byte1;
                bytes[offset + 2] = byte2;
                bytes[offset + 3] = byte3;
            }
        }
    }

    public static int WriteInt32(ref byte[] bytes, int offset, int value)
    {
        return WriteInt32(ref bytes, offset, value, System.BitConverter.IsLittleEndian);
    }

    public static int WriteInt32(ref byte[] bytes, int offset, int value, bool isLittleEndian)
    {
        var holder = new IntegerHolder(value);
        holder.Write(ref bytes, offset, isLittleEndian);
        return 4;
    }

    public static int ReadInt32(ref byte[] bytes, int offset, out int value)
    {
        return ReadInt32(ref bytes, offset, out value, System.BitConverter.IsLittleEndian);
    }

    public static int ReadInt32(ref byte[] bytes, int offset, out int value, bool isLittleEndian)
    {
        var holder = new IntegerHolder(ref bytes, offset, isLittleEndian);
        value = holder.Int32;
        return 4;
    }

    public static int WriteUInt32(ref byte[] bytes, int offset, uint value)
    {
        return WriteUInt32(ref bytes, offset, value, System.BitConverter.IsLittleEndian);
    }

    public static int WriteUInt32(ref byte[] bytes, int offset, uint value, bool isLittleEndian)
    {
        var holder = new IntegerHolder(value);
        holder.Write(ref bytes, offset, isLittleEndian);
        return 4;
    }

    public static int ReadUInt32(ref byte[] bytes, int offset, out uint value)
    {
        return ReadUInt32(ref bytes, offset, out value, System.BitConverter.IsLittleEndian);
    }

    public static int ReadUInt32(ref byte[] bytes, int offset, out uint value, bool isLittleEndian)
    {
        var holder = new IntegerHolder(ref bytes, offset, isLittleEndian);
        value = holder.UInt32;
        return 4;
    }
    //public static int WriteInt32(ref byte[] bytes, int offset, int value)
    //{
    //    return WriteInt32(ref bytes, offset, value, System.BitConverter.IsLittleEndian);
    //}

    //public static int WriteInt32(ref byte[] bytes, int offset, int value, bool isLittleEndian)
    //{
    //    if (System.BitConverter.IsLittleEndian != isLittleEndian) {
    //        bytes[offset + 0] = unchecked((byte)(value >> 24));
    //        bytes[offset + 1] = unchecked((byte)(value >> 16));
    //        bytes[offset + 2] = unchecked((byte)(value >> 8));
    //        bytes[offset + 3] = unchecked((byte)value);
    //    }
    //    else {
    //        bytes[offset + 0] = unchecked((byte)value);
    //        bytes[offset + 1] = unchecked((byte)(value >> 8));
    //        bytes[offset + 2] = unchecked((byte)(value >> 16));
    //        bytes[offset + 3] = unchecked((byte)(value >> 24));
    //    }

    //    return 4;
    //}

    //public static int ReadInt32(ref byte[] bytes, int offset, out int value)
    //{
    //    return ReadInt32(ref bytes, offset, out value, System.BitConverter.IsLittleEndian);
    //}

    //public static int ReadInt32(ref byte[] bytes, int offset, out int value, bool isLittleEndian)
    //{
    //    unchecked {
    //        if (System.BitConverter.IsLittleEndian != isLittleEndian)
    //            value = (bytes[offset] << 24) | (bytes[offset + 1] << 16) | (bytes[offset + 2] << 8) | bytes[offset + 3];
    //        else
    //            value = bytes[offset] | (bytes[offset + 1] << 8) | (bytes[offset + 2] << 16) | (bytes[offset + 3] << 24);
    //    }
    //    return 4;
    //}

    [StructLayout(LayoutKind.Explicit)]
    internal struct FloatHolder
    {
        [FieldOffset(0)]
        public readonly float Float32;

        [FieldOffset(0)]
        public readonly byte byte0;
        [FieldOffset(1)]
        public readonly byte byte1;
        [FieldOffset(2)]
        public readonly byte byte2;
        [FieldOffset(3)]
        public readonly byte byte3;

        // encoding order is based on system
        public FloatHolder(float value)
        {
            this = default;
            this.Float32 = value;
        }

        // encoding order is based on stream
        public FloatHolder(ref byte[] bytes, int offset, bool isLittleEndian)
        {
            this = default;

            if (System.BitConverter.IsLittleEndian != isLittleEndian) {
                this.byte0 = bytes[offset + 3];
                this.byte1 = bytes[offset + 2];
                this.byte2 = bytes[offset + 1];
                this.byte3 = bytes[offset];
            }
            else {
                this.byte0 = bytes[offset];
                this.byte1 = bytes[offset + 1];
                this.byte2 = bytes[offset + 2];
                this.byte3 = bytes[offset + 3];
            }
        }

        public void Write(ref byte[] bytes, int offset, bool isLittleEndian)
        {
            if (System.BitConverter.IsLittleEndian != isLittleEndian) {
                bytes[offset + 0] = byte3;
                bytes[offset + 1] = byte2;
                bytes[offset + 2] = byte1;
                bytes[offset + 3] = byte0;
            }
            else {
                bytes[offset + 0] = byte0;
                bytes[offset + 1] = byte1;
                bytes[offset + 2] = byte2;
                bytes[offset + 3] = byte3;
            }
        }
    }

    public static int WriteFloat32(ref byte[] bytes, int offset, float value)
    {
        return WriteFloat32(ref bytes, offset, value, System.BitConverter.IsLittleEndian);
    }

    public static int WriteFloat32(ref byte[] bytes, int offset, float value, bool isLittleEndian)
    {
        var holder = new FloatHolder(value);
        holder.Write(ref bytes, offset, isLittleEndian);
        return 4;
    }

    public static int ReadFloat32(ref byte[] bytes, int offset, out float value)
    {
        return ReadFloat32(ref bytes, offset, out value, System.BitConverter.IsLittleEndian);
    }

    public static int ReadFloat32(ref byte[] bytes, int offset, out float value, bool isLittleEndian)
    {
        var holder = new FloatHolder(ref bytes, offset, isLittleEndian);
        value = holder.Float32;
        return 4;
    }

    public static int WriteShort16(ref byte[] bytes, int offset, short value)
    {
        return WriteShort16(ref bytes, offset, value, System.BitConverter.IsLittleEndian);
    }

    public static int WriteShort16(ref byte[] bytes, int offset, short value, bool isLittleEndian)
    {
        if (System.BitConverter.IsLittleEndian != isLittleEndian) {
            bytes[offset] = unchecked((byte)(value >> 8));
            bytes[offset + 1] = unchecked((byte)value);
        }
        else {
            bytes[offset] = unchecked((byte)value);
            bytes[offset + 1] = unchecked((byte)(value >> 8));
        }
        return 2;
    }

    public static int ReadShort16(ref byte[] bytes, int offset, out short value)
    {
        return ReadShort16(ref bytes, offset, out value, System.BitConverter.IsLittleEndian);
    }

    public static int ReadShort16(ref byte[] bytes, int offset, out short value, bool isLittleEndian)
    {
        unchecked {
            if (System.BitConverter.IsLittleEndian != isLittleEndian)
                value = (short)((bytes[offset] << 8) | bytes[offset + 1]);
            else
                value = (short)(bytes[offset] | (bytes[offset + 1] << 8));
        }
        return 2;
    }

    public static int WriteByte(ref byte[] bytes, int offset, byte value)
    {
        bytes[offset] = value;
        return 1;
    }

    public static int ReadByte(ref byte[] bytes, int offset, out byte value)
    {
        value = bytes[offset];
        return 1;
    }

    public static int WriteBytes(ref byte[] bytes, int offset, ref byte[] value)
    {
        int startOffset = offset;
        offset += WriteInt32(ref bytes, offset, value.Length);
        System.Buffer.BlockCopy(value, 0, bytes, offset, value.Length);
        offset += value.Length;
        return offset - startOffset;
    }

    public static int ReadBytes(ref byte[] bytes, int offset, ref byte[] value)
    {
        int startOffset = offset;
        offset += ReadInt32(ref bytes, offset, out int arraySize);
        if (value == null || value.Length < arraySize)
            value = new byte[arraySize];
        System.Buffer.BlockCopy(bytes, offset, value, 0, arraySize);
        offset += arraySize;
        return offset - startOffset;
    }

    public static int WriteString(ref byte[] bytes, int offset, string value)
    {
        int startOffset = offset;
        int strLength = Encoding.UTF8.GetBytes(value, 0, value.Length, bytes, offset + 4);
        offset += WriteInt32(ref bytes, offset, strLength);
        offset += strLength;
        return offset - startOffset;
    }

    public static int ReadString(ref byte[] bytes, int offset, out string value)
    {
        int startOffset = offset;
        offset += ReadInt32(ref bytes, offset, out int strLength);
        value = Encoding.UTF8.GetString(bytes, offset, strLength);
        offset += strLength;
        return offset - startOffset;
    }

#if MessagePack
    public static int WriteStringBuffer(ref byte[] bytes, int offset, ref StringBuffer value)
    {
        int startOffset = offset;
        offset += WriteInt32(ref bytes, offset, value.bufferSize);
        offset += WriteBytes(ref bytes, offset, ref value.buffer);
        return offset - startOffset;
    }

    public static int ReadStringBuffer(ref byte[] bytes, int offset, ref StringBuffer value)
    {
        int startOffset = offset;
        offset += ReadInt32(ref bytes, offset, out value.bufferSize);
        offset += ReadBytes(ref bytes, offset, ref value.buffer);
        return offset - startOffset;
    }
#endif
}

#if MessagePack
[MessagePack.MessagePackObject]
public struct StringBuffer
{
    public const int DefaultBufferSize = 128;
    const int SizeOffset = 4;

    [MessagePack.Key(0)]
    public int bufferSize;
    [MessagePack.Key(1)]
    public byte[] buffer; // includes string length at first 4bytes

    public StringBuffer(int bufferSize = -1)
    {
        if (bufferSize > 0) {
            this.bufferSize = bufferSize;
            buffer = new byte[this.bufferSize];
        }
        else {
            this.bufferSize = DefaultBufferSize;
            buffer = new byte[this.bufferSize];
        }
    }

    public StringBuffer(string value)
    {
        int strLength = System.Text.UTF8Encoding.UTF8.GetByteCount(value);
        bufferSize = strLength + SizeOffset;
        buffer = new byte[bufferSize];
        BinaryManager.WriteString(ref buffer, 0, value);
    }

    public void SetString(string value)
    {
        int strLength = System.Text.UTF8Encoding.UTF8.GetByteCount(value);
        if (buffer == null || buffer.Length < strLength + SizeOffset) {
            bufferSize = strLength + SizeOffset;
            buffer = new byte[bufferSize];
        }
        BinaryManager.WriteString(ref buffer, 0, value);
    }

    public override string ToString()
    {
        BinaryManager.ReadString(ref buffer, 0, out string result);
        return result;
    }
}
#endif