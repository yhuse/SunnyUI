using System;
using System.Linq;

namespace Sunny.UI
{
    public static class MapperEx
    {
        private static void Mapper<T>(T source, T dest)
        {
            var listSource = source.GetType().GetNeedProperties().ToDictionary(prop => prop.Name);
            var listDest = source.GetType().GetNeedProperties().ToDictionary(prop => prop.Name);

            foreach (var item in listDest)
            {
                if (listSource.ContainsKey(item.Key))
                {
                    var sourceInfo = listSource[item.Key];
                    object sourceValue = sourceInfo.GetValue(source, null);
                    Type sourceType = sourceInfo.PropertyType;

                    Type destType = item.Value.PropertyType;
                    var destInfo = item.Value;

                    if (sourceType.IsValueType)
                    {
                        destInfo.SetValue(dest, sourceValue, null);
                    }
                    else
                    {

                    }
                }
            }
        }

        public static void MapperTo<T>(this T source, T dest)
        {
            Mapper(source, dest);
        }

        public static void MapperFrom<T>(this T dest, T source)
        {
            Mapper(source, dest);
        }
    }
}
