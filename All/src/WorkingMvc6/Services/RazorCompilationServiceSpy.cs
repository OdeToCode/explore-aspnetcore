using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WorkingMvc6.Services
{
    public class RazorCompilationServiceSpy : IRazorCompilationService
    {
        private IRazorCompilationService _inner;
        private IList<CompileEntry> _log;

        public RazorCompilationServiceSpy(ICompilationService compilationService,
                                          IMvcRazorHost razorHost, 
                                          IRazorViewEngineFileProviderAccessor fileProviderAccessor,
                                          ILoggerFactory logger)
        {
            _inner = new RazorCompilationService(compilationService, razorHost, fileProviderAccessor, logger);
            _log = new List<CompileEntry>();
        }

        public CompilationResult Compile(RelativeFileInfo fileInfo)
        {
            var result = _inner.Compile(fileInfo);
            _log.Add(new CompileEntry { FileInfo = fileInfo, Result = result });
            return result;
        }

        public IEnumerable<CompileEntry> CompilationLog
        {
            get
            {
                return _log;
            }
        }

        public class CompileEntry
        {
            public RelativeFileInfo FileInfo { get; set; }
            public CompilationResult Result { get; set; }
        }
    }
}
