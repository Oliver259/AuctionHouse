namespace AuctionHouse.Tests
{
    /// <summary>
    /// Provides a way to redirect console input/output for testing console applications
    /// without requiring actual user interaction.
    /// </summary>
    public class ConsoleRedirector : IDisposable
    {
        // Static lock object ensures thread-safety across all test instances
        // This prevents multiple tests from simultaneously modifying console streams
        private static readonly object ConsoleLock = new();
        
        // Store the original streams so we can restore them later
        private readonly TextReader _originalIn;
        private readonly TextWriter _originalOut;
        
        // Track if this instance has been disposed
        private bool _disposed;

        // Expose the redirected streams for test validation
        private StringReader inputReader { get; set; }
        public StringWriter outputWriter { get; private set; }

        /// <summary>
        /// Creates a new console redirection with simulated input
        /// </summary>
        /// <param name="input">The simulated user input to feed into the console</param>
        public ConsoleRedirector(string input)
        {
            // Ensure input ends with newline for proper ReadLine() behavior
            if (!input.EndsWith("\n"))
                input += "\n";
                
            // Acquire exclusive access to console - blocks other tests from
            // modifying console until this test releases the lock
            Monitor.Enter(ConsoleLock);
            
            try
            {
                // Save original console streams
                _originalIn = Console.In;
                _originalOut = Console.Out;
                
                // Create new in-memory streams for testing
                inputReader = new StringReader(input);
                outputWriter = new StringWriter();
                
                // Redirect console I/O to our in-memory streams
                Console.SetIn(inputReader);
                Console.SetOut(outputWriter);
            }
            catch
            {
                // If setup fails, release the lock and propagate the exception
                Monitor.Exit(ConsoleLock);
                throw;
            }
        }

        /// <summary>
        /// Restores the original console streams when the test is complete
        /// </summary>
        public void Dispose()
        {
            // Only dispose once, even if called multiple times
            if (!_disposed)
            {
                try
                {
                    // Restore original console streams
                    Console.SetIn(_originalIn);
                    Console.SetOut(_originalOut);
                }
                catch (Exception)
                {
                    // Ignore exceptions during cleanup - might already be disposed
                    // or modified by another test
                }
                
                try
                {
                    // Clean up the in-memory streams
                    inputReader.Dispose();
                    outputWriter.Dispose();
                }
                catch (Exception)
                {
                    // Ignore cleanup errors
                }
                
                // Mark as disposed to prevent double-disposal
                _disposed = true;
                
                // Release the lock so other tests can run
                Monitor.Exit(ConsoleLock);
            }
        }
    }
}