/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UBigEndianBinaryWriter.cs
 * 文件说明: 大端字节序的二进制写入器
 * 当前版本: V3.0
 * 创建日期: 2021-12-15
 *
 * 2021-12-15: V3.0.9 增加文件说明
******************************************************************************/


using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Text;

namespace System.IO
{
    // This abstract base class represents a writer that can write
    // primitives to an arbitrary stream. A subclass can override methods to
    // give unique encodings.
    [Serializable]
    [Runtime.InteropServices.ComVisible(true)]
    public class BigEndianBinaryWriter : IDisposable
    {
        public static readonly BigEndianBinaryWriter Null = new BigEndianBinaryWriter();

        protected Stream OutStream;
        private byte[] _buffer;    // temp space for writing primitives to.
        private Encoding _encoding;
        private Encoder _encoder;

        [OptionalField]  // New in .NET FX 4.5.  False is the right default value.
        private bool _leaveOpen;

        // This field should never have been serialized and has not been used since before v2.0.
        // However, this type is serializable, and we need to keep the field name around when deserializing.
        // Also, we'll make .NET FX 4.5 not break if it's missing.
#pragma warning disable 169
        [OptionalField]
        private char[] _tmpOneCharBuffer;
#pragma warning restore 169

        // Perf optimization stuff
        private byte[] _largeByteBuffer;  // temp space for writing chars.
        private int _maxChars;   // max # of chars we can put in _largeByteBuffer
        // Size should be around the max number of chars/string * Encoding's max bytes/char
        private const int LargeByteBufferSize = 256;

        // Protected default constructor that sets the output stream
        // to a null stream (a bit bucket).
        protected BigEndianBinaryWriter()
        {
            OutStream = Stream.Null;
            _buffer = new byte[16];
            _encoding = new UTF8Encoding(false, true);
            _encoder = _encoding.GetEncoder();
        }

        public BigEndianBinaryWriter(Stream output) : this(output, new UTF8Encoding(false, true), false)
        {
        }

        public BigEndianBinaryWriter(Stream output, Encoding encoding) : this(output, encoding, false)
        {
        }

        public BigEndianBinaryWriter(Stream output, Encoding encoding, bool leaveOpen)
        {
            if (output == null)
                throw new ArgumentNullException("output");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (!output.CanWrite)
                throw new ArgumentException("Argument_StreamNotWritable");
            Contract.EndContractBlock();

            OutStream = output;
            _buffer = new byte[16];
            _encoding = encoding;
            _encoder = _encoding.GetEncoder();
            _leaveOpen = leaveOpen;
        }

        // Closes this writer and releases any system resources associated with the
        // writer. Following a call to Close, any operations on the writer
        // may raise exceptions. 
        public void Close()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_leaveOpen)
                    OutStream.Flush();
                else
                    OutStream.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        /*
         * Returns the stream associate with the writer. It flushes all pending
         * writes before returning. All subclasses should override Flush to
         * ensure that all buffered data is sent to the stream.
         */
        public Stream BaseStream
        {
            get
            {
                Flush();
                return OutStream;
            }
        }

        // Clears all buffers for this writer and causes any buffered data to be
        // written to the underlying device. 
        public void Flush()
        {
            OutStream.Flush();
        }

        public long Seek(int offset, SeekOrigin origin)
        {
            return OutStream.Seek(offset, origin);
        }

        // Writes a boolean to this stream. A single byte is written to the stream
        // with the value 0 representing false or the value 1 representing true.
        public void Write(bool value)
        {
            _buffer[0] = (byte)(value ? 1 : 0);
            OutStream.Write(_buffer, 0, 1);
        }

        // Writes a byte to this stream. The current position of the stream is
        // advanced by one.
        public void Write(byte value)
        {
            OutStream.WriteByte(value);
        }

        // Writes a signed byte to this stream. The current position of the stream 
        // is advanced by one.
        public void Write(sbyte value)
        {
            OutStream.WriteByte((byte)value);
        }

        // Writes a byte array to this stream.
        // 
        // This default implementation calls the Write(Object, int, int)
        // method to write the byte array.
        public void Write(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");
            Contract.EndContractBlock();
            OutStream.Write(buffer, 0, buffer.Length);
        }

        // Writes a section of a byte array to this stream.
        //
        // This default implementation calls the Write(Object, int, int)
        // method to write the byte array.
        public void Write(byte[] buffer, int index, int count)
        {
            OutStream.Write(buffer, index, count);
        }


        // Writes a character to this stream. The current position of the stream is
        // advanced by two.
        // Note this method cannot handle surrogates properly in UTF-8.
        [Security.SecuritySafeCritical]  // auto-generated
        public unsafe void Write(char ch)
        {
            if (Char.IsSurrogate(ch))
                throw new ArgumentException("Arg_SurrogatesNotAllowedAsSingleChar");
            Contract.EndContractBlock();

            Contract.Assert(_encoding.GetMaxByteCount(1) <= 16, "_encoding.GetMaxByteCount(1) <= 16)");
            int numBytes;
            fixed (byte* pBytes = _buffer)
            {
                numBytes = _encoder.GetBytes(&ch, 1, pBytes, _buffer.Length, true);
            }
            OutStream.Write(_buffer, 0, numBytes);
        }

        // Writes a character array to this stream.
        // 
        // This default implementation calls the Write(Object, int, int)
        // method to write the character array.
        public void Write(char[] chars)
        {
            if (chars == null)
                throw new ArgumentNullException("chars");
            Contract.EndContractBlock();

            byte[] bytes = _encoding.GetBytes(chars, 0, chars.Length);
            OutStream.Write(bytes, 0, bytes.Length);
        }

        // Writes a section of a character array to this stream.
        //
        // This default implementation calls the Write(Object, int, int)
        // method to write the character array.
        public void Write(char[] chars, int index, int count)
        {
            byte[] bytes = _encoding.GetBytes(chars, index, count);
            OutStream.Write(bytes, 0, bytes.Length);
        }


        // Writes a double to this stream. The current position of the stream is
        // advanced by eight.
        [Security.SecuritySafeCritical]  // auto-generated
        public unsafe void Write(double value)
        {
            ulong TmpValue = *(ulong*)&value;
            _buffer[7] = (byte)TmpValue;
            _buffer[6] = (byte)(TmpValue >> 8);
            _buffer[5] = (byte)(TmpValue >> 16);
            _buffer[4] = (byte)(TmpValue >> 24);
            _buffer[3] = (byte)(TmpValue >> 32);
            _buffer[2] = (byte)(TmpValue >> 40);
            _buffer[1] = (byte)(TmpValue >> 48);
            _buffer[0] = (byte)(TmpValue >> 56);
            OutStream.Write(_buffer, 0, 8);
        }

        // Writes a two-byte signed integer to this stream. The current position of
        // the stream is advanced by two.
        public void Write(short value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            OutStream.Write(_buffer, 0, 2);
        }

        // Writes a two-byte unsigned integer to this stream. The current position
        // of the stream is advanced by two.
        public void Write(ushort value)
        {
            _buffer[1] = (byte)value;
            _buffer[0] = (byte)(value >> 8);
            OutStream.Write(_buffer, 0, 2);
        }

        // Writes a four-byte signed integer to this stream. The current position
        // of the stream is advanced by four.
        public void Write(int value)
        {
            _buffer[3] = (byte)value;
            _buffer[2] = (byte)(value >> 8);
            _buffer[1] = (byte)(value >> 16);
            _buffer[0] = (byte)(value >> 24);
            OutStream.Write(_buffer, 0, 4);
        }

        // Writes a four-byte unsigned integer to this stream. The current position
        // of the stream is advanced by four.
        public void Write(uint value)
        {
            _buffer[3] = (byte)value;
            _buffer[2] = (byte)(value >> 8);
            _buffer[1] = (byte)(value >> 16);
            _buffer[0] = (byte)(value >> 24);
            OutStream.Write(_buffer, 0, 4);
        }

        // Writes an eight-byte signed integer to this stream. The current position
        // of the stream is advanced by eight.
        public void Write(long value)
        {
            _buffer[7] = (byte)value;
            _buffer[6] = (byte)(value >> 8);
            _buffer[5] = (byte)(value >> 16);
            _buffer[4] = (byte)(value >> 24);
            _buffer[3] = (byte)(value >> 32);
            _buffer[2] = (byte)(value >> 40);
            _buffer[1] = (byte)(value >> 48);
            _buffer[0] = (byte)(value >> 56);
            OutStream.Write(_buffer, 0, 8);
        }

        // Writes an eight-byte unsigned integer to this stream. The current 
        // position of the stream is advanced by eight.
        public void Write(ulong value)
        {
            _buffer[7] = (byte)value;
            _buffer[6] = (byte)(value >> 8);
            _buffer[5] = (byte)(value >> 16);
            _buffer[4] = (byte)(value >> 24);
            _buffer[3] = (byte)(value >> 32);
            _buffer[2] = (byte)(value >> 40);
            _buffer[1] = (byte)(value >> 48);
            _buffer[0] = (byte)(value >> 56);
            OutStream.Write(_buffer, 0, 8);
        }

        // Writes a float to this stream. The current position of the stream is
        // advanced by four.
        [Security.SecuritySafeCritical]  // auto-generated
        public unsafe void Write(float value)
        {
            uint TmpValue = *(uint*)&value;
            _buffer[3] = (byte)TmpValue;
            _buffer[2] = (byte)(TmpValue >> 8);
            _buffer[1] = (byte)(TmpValue >> 16);
            _buffer[0] = (byte)(TmpValue >> 24);
            OutStream.Write(_buffer, 0, 4);
        }


        // Writes a length-prefixed string to this stream in the BinaryWriter's
        // current Encoding. This method first writes the length of the string as 
        // a four-byte unsigned integer, and then writes that many characters 
        // to the stream.
        [Security.SecuritySafeCritical]  // auto-generated
        public unsafe void Write(String value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            Contract.EndContractBlock();

            int len = _encoding.GetByteCount(value);
            Write7BitEncodedInt(len);

            if (_largeByteBuffer == null)
            {
                _largeByteBuffer = new byte[LargeByteBufferSize];
                _maxChars = _largeByteBuffer.Length / _encoding.GetMaxByteCount(1);
            }

            if (len <= _largeByteBuffer.Length)
            {
                //Contract.Assert(len == _encoding.GetBytes(chars, 0, chars.Length, _largeByteBuffer, 0), "encoding's GetByteCount & GetBytes gave different answers!  encoding type: "+_encoding.GetType().Name);
                _encoding.GetBytes(value, 0, value.Length, _largeByteBuffer, 0);
                OutStream.Write(_largeByteBuffer, 0, len);
            }
            else
            {
                // Aggressively try to not allocate memory in this loop for
                // runtime performance reasons.  Use an Encoder to write out 
                // the string correctly (handling surrogates crossing buffer
                // boundaries properly).  
                int charStart = 0;
                int numLeft = value.Length;
#if _DEBUG
                int totalBytes = 0;
#endif
                while (numLeft > 0)
                {
                    // Figure out how many chars to process this round.
                    int charCount = (numLeft > _maxChars) ? _maxChars : numLeft;
                    int byteLen;

                    checked
                    {
                        if (charStart < 0 || charCount < 0 || charStart + charCount > value.Length)
                        {
                            throw new ArgumentOutOfRangeException(nameof(charCount));
                        }

                        fixed (char* pChars = value)
                        {
                            fixed (byte* pBytes = _largeByteBuffer)
                            {
                                byteLen = _encoder.GetBytes(pChars + charStart, charCount, pBytes, _largeByteBuffer.Length, charCount == numLeft);
                            }
                        }
                    }
#if _DEBUG
                    totalBytes += byteLen;
                    Contract.Assert (totalBytes <= len && byteLen <= _largeByteBuffer.Length, "BinaryWriter::Write(String) - More bytes encoded than expected!");
#endif
                    OutStream.Write(_largeByteBuffer, 0, byteLen);
                    charStart += charCount;
                    numLeft -= charCount;
                }
#if _DEBUG
                Contract.Assert(totalBytes == len, "BinaryWriter::Write(String) - Didn't write out all the bytes!");
#endif
            }
        }

        protected void Write7BitEncodedInt(int value)
        {
            // Write out an int 7 bits at a time.  The high bit of the byte,
            // when on, tells reader to continue reading more bytes.
            uint v = (uint)value;   // support negative numbers
            while (v >= 0x80)
            {
                Write((byte)(v | 0x80));
                v >>= 7;
            }
            Write((byte)v);
        }
    }
}