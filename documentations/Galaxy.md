# Galaxy 类

Galaxy 类代表一个包含多个星体及其路径的星系。

## 属性

- **numStars** (`int`): 星系的数量。
- **width** (`int`): 星系的宽度。
- **height** (`int`): 星系的高度。
- **stars** (`List<Star.Star>`): 一个包含所有星体的列表。
- **paths** (`List<Path.Path>`): 一个包含所有路径的列表。

## 构造函数

- `Galaxy(int numStars, int width, int height)`: 使用指定的星体数量、宽度和高度创建一个新的 Galaxy 实例。

## 方法

### InitGrid
**描述**: 初始化地图分区。
- `private void InitGrid()`

### CloestStars
**描述**: 寻找距离给定星体最近的 n 个星体。
- `public Star.Star[] CloestStars(Star.Star star, int n)`

**参数**:
  - `star` (`Star.Star`): 参考的星体。
  - `n` (`int`): 想要获取的星体数量。

### GenerateStarsAndPaths
**描述**: 生成星体和它们之间的路径。
- `public void GenerateStarsAndPaths()`

### Find
**描述**: 查找给定星体的代表星。
- `private Star.Star Find(Star.Star star)`

**参数**:
  - `star` (`Star.Star`): 需要查找其代表星的星体。

### Union
**描述**: 将两个星体合并为一个集合。
- `private void Union(Star.Star star1, Star.Star star2)`

**参数**:
  - `star1` (`Star.Star`): 第一个星体。
  - `star2` (`Star.Star`): 第二个星体。
