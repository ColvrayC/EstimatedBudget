using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using Microsoft.Practices.Unity;
using System.Reflection;
using System.ComponentModel;

namespace EstimatedBudget.Helpers
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RaisePropertyChanged : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new PropertyChangedCallHandler();
        }
    }
    public class PropertyChangedCallHandler : ICallHandler
    {
        #region ICallHandler Members

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            bool shouldRaise = ShouldRaiseEvent(input);
            IMethodReturn res = getNext()(input, getNext);

            if (res.Exception == null && shouldRaise)
            {
                RaiseEvent(input);
            }

            return res;
        }

        public int Order
        {
            get;
            set;
        }

        #endregion

        private bool ShouldRaiseEvent(IMethodInvocation input)
        {
            MethodBase methodBase = input.MethodBase;

            //Is the method a property setter?
            if (!methodBase.IsSpecialName || !methodBase.Name.StartsWith("set_"))
            {
                return false;
            }

            //Get the name of the property out so we can use it to raise a 
            //property changed event
            string propertyName = methodBase.Name.Substring(4);

            //Retrieve the property getter
            PropertyInfo property = methodBase.ReflectedType.GetProperty(propertyName);
            MethodInfo getMethod = property.GetGetMethod();

            //IF the property has no get method, we don't care
            if (getMethod == null)
            {
                return false;
            }

            //Get the current value out
            object oldValue = getMethod.Invoke(input.Target, null);

            //Get the updated value
            object value = input.Arguments[0];


            //Is the new value null?
            if (value != null)
            {
                //Is the new value different from the old value?
                if (value.Equals(oldValue) == false)
                {
                    return true;
                }
            }
            else
            {
                //Is the new value (null) different from the 
                //old value (non-null)?
                // if (value != oldValue)
                // {
                return true;
                //}
            }

            return false;
        }

        private void RaiseEvent(IMethodInvocation input)
        {
            FieldInfo field = null;

            //Get a reference to the PropertyChanged event out of the current 
            //type or one of the base types
            var type = input.MethodBase.ReflectedType;
            while (field == null && type != null)
            {
                field = type.GetField("PropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                type = type.BaseType;
            }

            //If we found the PropertyChanged event
            if (field != null)
            {
                //Get the event handler if there is one
                var evt = field.GetValue(input.Target) as MulticastDelegate;
                if (evt != null)
                {
                    //Get the property name out
                    string propertyName = input.MethodBase.Name.Substring(4);
                    //Invoke the property changed event handlers
                    evt.DynamicInvoke(input.Target, new PropertyChangedEventArgs(propertyName));
                }
            }
        }
    }
}
