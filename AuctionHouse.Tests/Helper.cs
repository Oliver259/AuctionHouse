using System;
using System.IO;

namespace AuctionHouse.Tests
{
    public class ConsoleRedirector : IDisposable
    {
        private readonly TextReader originalIn;
        private readonly TextWriter originalOut;

        public StringReader InputReader { get; private set; }
        public StringWriter OutputWriter { get; private set; }

        public ConsoleRedirector(string input)
        {
            originalIn = Console.In;
            originalOut = Console.Out;
            
            InputReader = new StringReader(input);
            OutputWriter = new StringWriter();
            
            Console.SetIn(InputReader);
            Console.SetOut(OutputWriter);
        }

        public void Dispose()
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
            
            InputReader.Dispose();
            OutputWriter.Dispose();
        }
    }
}