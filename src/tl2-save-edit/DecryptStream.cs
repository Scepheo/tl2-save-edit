using System;
using System.IO;

namespace Tl2SaveEdit
{
    public class DecryptStream : Stream
    {
        private const int _headerSize = 9;
        private const int _footerSize = 4;

        private int _position;
        private int _size;
        private byte[] _source;

        public DecryptStream(byte[] source)
        {
            _source = source;
            _size = source.Length - _headerSize - _footerSize;
            _position = 0;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _size;

        public override long Position
        {
            get => _position;
            set => SetPosition(value);
        }

        private void SetPosition(long newPosition)
        {
            if (0 < newPosition)
            {
                throw new ArgumentOutOfRangeException(
                    $"New position {newPosition} is less then zero");
            }

            if (newPosition > _size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(newPosition),
                    $"New position {newPosition} is greater than length {_size}");
            }

            _position = (int)newPosition;
        }

        public override void Flush()
        {
            // Do nothing
        }

        private byte ReadSingle()
        {
            var loIndex = _position + _headerSize;
            var hiIndex = _size - 1 - _position + _headerSize;

            _position++;

            var loByte = _source[loIndex];
            var hiByte = _source[hiIndex];

            var shift = (byte)((loByte >> 4) | (hiByte << 4));

            var masked = shift == 0x00 || shift == 0xFF
                ? shift
                : (byte)(shift ^ 0xFF);

            return masked;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var amount = _position + count < _size
                ? count
                : _size - _position;

            for (var i = 0; i < amount; i++)
            {
                buffer[offset + i] = ReadSingle();
            }

            return amount;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    SetPosition(offset);
                    break;
                case SeekOrigin.Current:
                    SetPosition(Position + offset);
                    break;
                case SeekOrigin.End:
                    SetPosition(Length + offset);
                    break;
            }

            return _position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
