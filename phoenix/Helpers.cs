using System;
using System.Linq.Expressions;

namespace phoenix
{
    class Helpers
    {
        /// <summary>
        /// Returns property name as string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyLambda"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }
        /// <summary>
        /// Returns class name as string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyLambda"></param>
        /// <returns></returns>
        public static string GetClassName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.ReflectedType.Name;
        }
    }
}
