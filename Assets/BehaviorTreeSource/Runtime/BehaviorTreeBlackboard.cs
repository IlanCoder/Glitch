using System;
using Characters;

namespace BehaviorTreeSource.Runtime {
    [Serializable]
    public class BehaviorTreeBlackboard {
        public CharacterManager targetCharacter;
    }
}