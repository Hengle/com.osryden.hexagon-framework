using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    public abstract class HexagonMapShape : IEnumerable<HexagonCoordinates>, IEnumerable, ISerializationCallbackReceiver
    {
        [SerializeField] private HexagonCoordinates m_Origin;
        [NonSerialized] private HashSet<HexagonCoordinates> m_Coordinates = null;

        protected HexagonMapShape(HexagonCoordinates origin)
        {
            m_Origin = origin;
        }

        public HexagonCoordinates Origin => m_Origin;

        private HashSet<HexagonCoordinates> Coordinates
        {
            get
            {
                if (m_Coordinates == null)
                    Initialize();

                return m_Coordinates;
            }
        }

        public IEnumerator<HexagonCoordinates> GetEnumerator()
        {
            return Coordinates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Initialize();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        private void Initialize()
        {
            m_Coordinates = new HashSet<HexagonCoordinates>();

            foreach (HexagonCoordinates coordinates in Shape())
                m_Coordinates.Add(Origin + coordinates);
        }

        protected abstract HashSet<HexagonCoordinates> Shape();
    }
}
