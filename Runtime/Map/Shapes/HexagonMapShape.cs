using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osryden.HexagonFramework
{
    public abstract class HexagonMapShape : IEnumerable<HexagonCoordinates>, IEnumerable, ISerializationCallbackReceiver
    {
        [SerializeField] private HexagonCoordinates m_Origin;
        [NonSerialized] private bool m_Initialized = false;
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
                if (!m_Initialized)
                {
                    m_Coordinates = new HashSet<HexagonCoordinates>();

                    foreach (HexagonCoordinates coordinates in Shape())
                        m_Coordinates.Add(Origin + coordinates);

                    m_Initialized = true;
                }

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
            m_Initialized = false;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        protected abstract HashSet<HexagonCoordinates> Shape();
    }
}
