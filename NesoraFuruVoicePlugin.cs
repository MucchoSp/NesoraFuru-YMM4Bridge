using YukkuriMovieMaker.Plugin.Voice;

namespace NesoraFuruYMM4Bridge
{
    public class NesoraFuruVoicePlugin : IVoicePlugin
    {
        /// <summary>
        /// プラグインの名前
        /// </summary>
        public string Name => "音諳零号機四型-YMM4ブリッジ";

        /// <summary>
        /// 声質一覧
        /// IVoiceSpeakerのインスタンスは毎回作成しても良い（キャッシュする必要はない）
        /// 呼び出しごとに増減しても良い
        /// </summary>
        public IEnumerable<IVoiceSpeaker> Voices => NesoraFuruVoiceSettings.Default.Speakers.Select(x => new NesoraFuruVoiceSpeaker(x));

        /// <summary>
        /// VoicesをUpdateVoicesAsync()で更新可能な場合はtrue。固定の場合はfalse。
        /// </summary>
        public bool CanUpdateVoices => true;

        /// <summary>
        /// Voicesのキャッシュが作成されている場合はtrue。作成されていない場合はfalse。
        /// falseの場合、YMM4起動時にUpdateVoicesAsync()が呼ばれる。
        /// </summary>
        public bool IsVoicesCached => NesoraFuruVoiceSettings.Default.IsCached;

        /// <summary>
        /// 音声一覧を再読込する。
        /// 再読込した結果は設定ファイルに保存し、キャッシュする必要がある。
        /// </summary>
        public Task UpdateVoicesAsync() => NesoraFuruVoiceSettings.Default.UpdateSpeakersAsync();
    }
}
