# Path 类

`Path` 类代表了两个星系之间的连接路径，其中包括它们的速度比率、是否为联盟路径、以及两个星系之间的距离。

## 导入的库

- `System.Collections`
- `System.Collections.Generic`
- `UnityEngine`
- `Star`

## 属性

- **star1** (`Star.Star`): 路径上的第一个星系。
- **star2** (`Star.Star`): 路径上的第二个星系。
- **speedRate** (`double`): 路径上的移动速度比率，默认为1.0。
- **unionPath** (`bool`): 表示该路径是否为联盟路径，默认为`false`。
- **distance** (`double`): 两个星系之间的距离。

## 构造函数

- `Path(Star.Star star1, Star.Star star2)`: 使用给定的两个星系创建一个新的 `Path` 实例，并计算这两个星系之间的距离。

---

