using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jareds.Utils
{
    public class LazyJaredValue<T>
    {

        private T m_Value;
        private bool m_Initialized = false;
        private InitializerDelegate m_Initializer;

        public delegate T InitializerDelegate();

        

        public LazyJaredValue(InitializerDelegate initializer)
        {
            m_Initializer = initializer;
        }


        public T value
        {
            get
            {
                ForceInit();
                return m_Value;
            }
            set
            {
                m_Initialized = true;
                m_Value = value;
            }
        }



        public void ForceInit()
        {
            if (!m_Initialized)
            {
                m_Value = m_Initializer();
                m_Initialized = true;
            }
        }

    }
}
