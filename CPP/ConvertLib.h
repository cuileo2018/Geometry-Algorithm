//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#ifndef CONVERTLIB_H
#define CONVERTLIB_H

#include <string>
#include "constHelper.h" // + 

namespace GeoLib
{
    class ConvertLib
    {
    public:
        static int GetInt32(const std::string& val);
        static int GetInt32(const std::string& val, int defaultValue);
        static double GetDouble(const std::string& val);
        static float GetFloat(const std::string& val);
        static int roundToInt(float val);

        static std::string geometryTypeToString(constHelper::GeometryType type); // 1.添加方法声明 2.删掉 const 3.删掉枚举类型前的 &
    };
}

#endif // CONVERTLIB_H
