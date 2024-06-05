namespace Terminal.Tools
{
    public static class RandomHexbetweenZeroAnd255
    {
        public static byte GetRandomByte()
        {
            Random random = new();
            int randomNumber = random.Next(0, 256);
            return (byte)randomNumber;
        }
    }
}
