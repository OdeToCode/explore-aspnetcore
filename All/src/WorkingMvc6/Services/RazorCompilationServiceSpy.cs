using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Mvc.Razor.Compilation;
using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;

namespace WorkingMvc6.Services
{
    public class RazorCompilationServiceSpy : IRazorCompilationService
    {
        private IRazorCompilationService _inner;
        private IList<CompileEntry> _log;

        public RazorCompilationServiceSpy(ICompilationService compilationService,
                                          IMvcRazorHost razorHost, 
                                          IOptions<RazorViewEngineOptions> options)
        {
            _inner = new RazorCompilationService(compilationService, razorHost, options);
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
