using System.IO;
using System.Runtime.InteropServices;

namespace NesoraFuruYMM4Bridge
{
    internal static class NesoraFuruBridge
    {
        private const string LibraryName = "NesoraFuruLib";
        private static readonly string PluginDirectory =
            Path.GetDirectoryName(typeof(NesoraFuruBridge).Assembly.Location)!;

        static NesoraFuruBridge()
        {
            NativeLibrary.SetDllImportResolver(typeof(NesoraFuruBridge).Assembly,
                (name, assembly, searchPath) => name == LibraryName
                    ? NativeLibrary.Load(Path.Combine(PluginDirectory, "lib", LibraryName + ".dll"))
                    : IntPtr.Zero);
        }

        [DllImport(LibraryName, EntryPoint = "nesora_synthesize_wav",
            CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        private static extern IntPtr SynthesizeWavNative(
            [MarshalAs(UnmanagedType.LPUTF8Str)] string dictionary,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string model,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string json,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string text,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string wav);

        public static void SynthesizeWav(string text, string filePath)
        {
            var resources = Path.Combine(PluginDirectory, "resources");
            var model = Path.Combine(resources, "voices", "mei_normal.htsvoice");
            if (!File.Exists(model))
                model = Path.Combine(resources, "mei_normal.htsvoice");

            var error = SynthesizeWavNative(
                Path.Combine(resources, "open_jtalk_dic_utf_8-1.11"),
                model, Path.Combine(PluginDirectory, "voice", "NesoraFuru.json"),
                text, filePath);
            if (error != IntPtr.Zero)
                throw new InvalidOperationException($"音諳ふるの音声合成に失敗しました。{Marshal.PtrToStringUTF8(error)}");
        }
    }
}
