using UnityEngine;

namespace Efficiency
{
    [CreateAssetMenu(fileName = "HeroDataConfiguration", menuName = "Configurations/HeroDataConfiguration")]
    public class HeroDataConfiguration : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        public Sprite Image { get => _sprite;}
        public string Name { get => _name;}
        public string Description { get => _description;}
    }
}