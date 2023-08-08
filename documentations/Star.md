# Star 类

`Star` 类代表了一个星系，其中包括其位置、资源、是否为黑洞、是否宜居等特性。

## 导入的库

- `System.Collections`
- `System.Collections.Generic`
- `UnityEngine`
- `MapResources`

## 属性

- **id** (`int`): 星系的ID。
- **type** (`int`): 星系的类型。其中 0 为资源型星系，1为黑洞。
- **pos** (`Vector2`): 星系的位置信息。
- **isLivable** (`bool`): 星系是否宜居。
- **isDestroyed** (`bool`): 星系是否被摧毁。
- **adj** (`List<Star>`): 记录相邻的星系。
- **resources** (`Dictionary<MapResources.Resources, int>`): 星系的资源数值，包括人口、食物、金属、能量和技术。
- **resTick** (`int`): 摧毁星系时可获取多少个tick的资源（私有属性）。
- **maxResValue** (`int`): 最大资源数值（私有属性）。
- **minResValue** (`int`): 最小资源数值（私有属性）。

## 构造函数

- `Star(int id, Vector2 pos, double livableRate, double blackholeRate)`: 使用给定的id、位置、宜居率和黑洞率创建一个新的 `Star` 实例。

## 方法

### fillResources

**描述**: 给一个星系填充资源。

- `public void fillResources()`

### destroy

**描述**: 摧毁星系时使用这个方法，返回摧毁后获得的资源数值并清空该星系资源生成。

- `public Dictionary<MapResources.Resources, int> destroy()`

---

