namespace libloaderapi.Domain.Services
{
    public interface IAnalyzerService
    {
        public bool Iter0(byte[] payload, out ulong result);

        public bool Iter1(byte[] payload, out ulong result);
    }

    public class AnalyzerService : IAnalyzerService
    {
        public unsafe bool Iter0(byte[] payload, out ulong result)
        {
            // Jmp CiInitalize
            fixed (byte* @base = &payload[1])
            {
                byte* p = @base;

                for (ulong i = 0; i < 100; i++, p += 0x1)
                {
                    if (((p[-1] & 0xfe) == 0xe8) && (((p[2] | p[3]) <= 0) || ((p[2] & p[3]) == 0xff)))
                    {
                        var t = p + 4 + *(int*)p;
                        if (t[0] == 0x48 && t[1] == 0x8b && t[2] == 0x05)
                            continue;

                        var j = i;
                        for (int k = 0; k < payload.Length / 2; k++)
                            j ^= payload[k];


                        result = j;
                        return true;
                    }
                }
            }

            result = 0;
            return false;
        }

        public unsafe bool Iter1(byte[] payload, out ulong result)
        {
            // CiOpts                           
            fixed (byte* @base = &payload[2])
            {
                var p = @base;
                for (ulong i = 0; i < 100; i++, p++)
                {
                    if (p[-2] == 0x89 && p[-1] == 0x0d && p[3] == 0xff)
                    {
                        var j = i;
                        for (int k = 0; k < payload.Length / 2; k++)
                            j ^= payload[k];

                        result = j;
                        return true;
                    }
                }
            }

            result = 0;
            return false;
        }
    }
}
