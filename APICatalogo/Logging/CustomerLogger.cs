namespace APICatalogo.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;

        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfig = config;
        }

        public bool IsEnabled(LogLevel logLevel) 
        {
            return logLevel == loggerConfig.LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state) 
        {
            return null;
        }       

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception
                )}";

            EscreverTextoNoArquivvo(mensagem);
        }

        private void EscreverTextoNoArquivvo(string mensagem)
        {
            try
            {
                string caminhoArquivoLog = @"d:\dados\log\LogTeste.txt";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
