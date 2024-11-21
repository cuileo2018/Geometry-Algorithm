//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#include <sstream>
#include "ConvertLib.h"
#include "constHelper.h"

using namespace std;

namespace GeoLib
{
    int ConvertLib::GetInt32(const std::string& val)
    {
        return GetInt32(val, 0);
    }

    int ConvertLib::GetInt32(const std::string& val, int defaultValue)
    {
        if (val.empty()) return defaultValue;

        int result = defaultValue;
        std::istringstream iss(val);
        if (!(iss >> result)) {
            return defaultValue;
        }
        return result;
    }

    double ConvertLib::GetDouble(const std::string& val)
    {
        if (val.empty()) return 0;

        double result = 0;
        std::istringstream iss(val);
        if (!(iss >> result)) {
            return 0;
        }
        return result;
    }

    float ConvertLib::GetFloat(const std::string& val)
    {
        if (val.empty()) return 0;

        float result = 0;
        std::istringstream iss(val);
        if (!(iss >> result)) {
            return 0;
        }
        return result;
    }

    int ConvertLib::roundToInt(float val)
    {
        return static_cast<int>(val + 0.5f);
    }

    string ConvertLib::geometryTypeToString(constHelper::GeometryType type) { // 添加类名
        switch (type) {
        case constHelper::GeometryType::Cuboid:
            return "Cuboid";
        case constHelper::GeometryType::Cylinder:
            return "Cylinder";
        case constHelper::GeometryType::Line:
            return "Line";
        case constHelper::GeometryType::Point:
            return "Point";
        default:
            return "Unknown";
        }
    }
}
