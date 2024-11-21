//=====================================================================
// 所述类库：基本几何体算法类库
// 版权声明：2024 九州创智科技有限公司  All Rights Reserved.
// 更新履历：2024.08 崔艳龙 创建
//============================================

#include "BaseGeometry.h"
#include "UUIDGenerator.h"

namespace GeoLib {
    BaseGeometry::BaseGeometry(std::shared_ptr<PointModel> centerPoint, std::shared_ptr<AngleModel> rotateAngle, const std::string& type)
        : _centerPoint(centerPoint), _rotateAngle(rotateAngle), _type(type) {
        _id = UUIDGenerator::generateUUID();
        _keyList = std::vector<PointGeometry>(); // 初始化_keyList
        _fsize = nullptr;
    }

    std::string BaseGeometry::getId() const {
        return _id;
    }

    void BaseGeometry::setId(const std::string& id) {
        _id = id;
    }

    std::shared_ptr<PointModel> BaseGeometry::getPosition() const {
        return _centerPoint;
    }

    void BaseGeometry::setPosition(std::shared_ptr<PointModel> centerPoint) {
        _centerPoint = centerPoint;
    }

    std::shared_ptr<AngleModel> BaseGeometry::getRotation() const {
        return _rotateAngle;
    }

    void BaseGeometry::setRotation(std::shared_ptr<AngleModel> rotateAngle) {
        _rotateAngle = rotateAngle;
    }

    std::vector<PointGeometry> BaseGeometry::getKeypoints() const {
        return _keyList;
    }

    std::shared_ptr<void> BaseGeometry::getFSize() const {
        return _fsize;
    }

    void BaseGeometry::setFSize(std::shared_ptr<void> fsize) {
        _fsize = fsize;
    }

    std::string BaseGeometry::getType() const {
        return _type;
    }

    void BaseGeometry::CreateKeyPoint() {
        if (_keyList.empty()) {
            _keyList = std::vector<PointGeometry>(); // 初始化_keyList
        }
        else {
            _keyList.clear();
        }
    }

    void BaseGeometry::Shifting() {
        // Implementation here
    }

    void BaseGeometry::Rotate() {
        // Implementation here
    }

    float BaseGeometry::ToGeometryDistance() {
        return 0.0f;
    }
}