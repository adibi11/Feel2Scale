using Feel2Scale.Data;

namespace Feel2Scale.Models
{
    public class EmotionPrompt
    {
        public string ?UserEmotion { get; set; }
        public ScaleData AiData { get; set; }
    }
}
