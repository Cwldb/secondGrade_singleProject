using UnityEngine;

namespace Assets.Blade.Feedbacks
{
    public abstract class FeedBack : MonoBehaviour
    {
        public abstract void CreateFeedback();
        public abstract void StopFeedback();
    }
}