using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Plugin.Voice;

namespace NesoraFuruYMM4Bridge
{
    internal class NesoraFuruVoiceParameter : VoiceParameterBase
    {
        double speed = 0;

        /// <summary>
        /// アイテム編集エリアに表示する音声の設定項目。
        /// [Display]と[TextBoxSlider]等のアイテム編集コントロール属性の2つを設定する必要があります。
        /// [TextBoxSlider]以外のアイテム編集コントロール属性の一覧はSamplePropertyEditorsプロジェクトを参照してください。
        /// </summary>
        [Display(Name = "話速", Description = "テキストの読み上げ速度")]
        [TextBoxSlider("F0", "", -10, 10, Delay = -1)]//Delayを-1にするとスライダーを操作し終えるまで反映しない。これがないとスライダーの操作中に音声の再生成処理が走ってしまう。
        [Range(-10, 10)]
        [DefaultValue(0)]
        public double Speed { get => speed; set => Set(ref speed, value); }
    }
}
