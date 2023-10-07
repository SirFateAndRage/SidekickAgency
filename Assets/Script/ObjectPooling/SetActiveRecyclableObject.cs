namespace ObjectPool
{

    public class SetActiveRecyclableObject : RecyclableObject
    {
        public override void OnRecycle()
        {
            gameObject.SetActive(false);
        }

        public override void OnRevived()
        {
            gameObject.SetActive(true);
        }
    }
}
