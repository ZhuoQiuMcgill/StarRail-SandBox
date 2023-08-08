### Star 类文档

#### 名称空间:
- System.Collections
- System.Collections.Generic
- UnityEngine
- MapResources

#### 简介:

`Star` 类用于模拟宇宙中的星系。每个星系都有特定的资源、属性和状态，如类型、是否宜居、是否已被摧毁等。

---

#### 属性:

- **id** (`int`): 星系的唯一标识符，只读。
- **type** (`int`): 星系的类型，0代表资源型星系，1代表黑洞，可读写。
- **pos** (`Vector2`): 星系的位置信息，可读写。
- **isLivable** (`bool`): 标识星系是否宜居，可读写。
- **isDestroyed** (`bool`): 标识星系是否已被摧毁，可读写。
- **adj** (`List<Star>`): 记录与该星系相邻的其他星系。
- **resources** (`Dictionary<MapResources.Resources, int>`): 星系中的资源列表和其对应数量。

---

#### 构造函数:

- **Star(int id, Vector2 pos, double livableRate, double blackholeRate)**:
    - 参数:
        - **id** (`int`): 星系的唯一标识符。
        - **pos** (`Vector2`): 星系的位置。
        - **livableRate** (`double`): 星系是宜居的概率。
        - **blackholeRate** (`double`): 星系是黑洞的概率。
    - 描述: 根据提供的参数初始化一个新的星系对象。

---

#### 公共方法:

- **fillResources()**:
    - 返回类型: `void`
    - 描述: 根据星系的类型填充资源。如果是黑洞，主要填充技术和能源。如果是资源型星系，填充能源和金属，如果还是宜居的，则额外填充食物资源。

- **destroy()**:
    - 返回类型: `Dictionary<MapResources.Resources, int>`
    - 描述: 摧毁该星系并返回由于摧毁而获得的资源数量。摧毁后，星系不再宜居，也无法再生成资源。

---

#### 私有字段:

- **resTick** (`int`): 定义摧毁星系时可获取多少个tick的资源，默认值为1000。
- **maxResValue** (`int`): 星系内部的最大资源数值，用于随机分配资源，其默认值为21。
- **minResValue** (`int`): 星系内部的最小资源数值，用于随机分配资源，其默认值为5。

---

注意: 当提到“星系”的时候，指的是`Star`类的一个实例。
