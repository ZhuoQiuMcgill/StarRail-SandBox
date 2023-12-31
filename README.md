# StarRail-SandBox ver 0.0.1
Last update: 2023/8/3

## 游戏介绍
在《StarRail-SandBox》中，玩家将扮演上帝，俯瞰浩瀚宇宙的演化。宇宙中有几个特定的派系，每
个派系都有其独特的文化、技术和策略。随着时间的流逝，银河将会经历各种不可预知的危机和事
件：黑洞的出现、星际战争、文明的崛起和衰退等。玩家可以选择观察或微妙地干涉，但目标是看
哪一个派系能适应所有的变故，最终度过压力测试，成为宇宙中最后的存活者。 

## 框架
<details>
  <summary>逻辑层</summary>

  - AI 行为逻辑：
    本作中的 AI 行为逻辑使用 崩坏星穹铁道 中不同命途派系的行为逻辑作为根据。其中所有的派系包括：毁灭，存护，巡猎，智识，虚无，同谐，丰饶，记忆，欢愉，纯美，繁育，神秘，均衡，开拓，贪饕。详细信息请见下方派系说明。 

  - 地图生成逻辑：
    本作中的地图设定为一个随机生成的银河系，采用 graph作为底层数据结构。其中的星系以及路径对应了两个主要的 graph 元素，vertex 和 edge。星系包括以下几种：资源星系，宜居星系以及黑洞；路径则是联通两个星系之间的道路，只有两个星系之间存在路径才能移动。当一个派系在一个星系中修建前哨站之后，这个星系将会划分到这个派系中。

  - 数值设定：
    本作的数值设定分为两种，一种是服务于地图以及派系资源的资源数值，另一种是服务于具备 AI 行为逻辑的个体数值。其中资源数值包括了人口，粮⾷，金属，能量，科技。粮⾷作为人口增长的主要指数，大部分产出于宜居星系，少部分产出于空间站等宇宙定居点。部分派系不需要粮⾷维系，例如毁灭派系，人口通过能量维系。金属与能量产出于资源星系。科技数值则通过人口数量以及派系独有科技增长倍率数值，其中某些派系的增长倍率会比较高，比如智识。 

  - 科技树设定：
    科技树分为两种，一种是每个派系都有自己独有的科技树，另一种是通用科技树，例如建造星门进行传送以减少路径之间移动所需要的时间，提高资源产出，减少人口维护成本等等。科技的解锁通过计算每个 tick 的科技值进行累计，达到科技解锁要求的值后自动解锁。

</details>

<details>
  <summary>表现层</summary>
  表现层主要提供地图渲染功能，数值显示以及摄像机操作。其中地图渲染则包括渲染星系和路径，渲染派系范围；数值显示包括点击星系后提供星系数值，点击派系显示派系资源数值，点击个体单位显示个体单位数值。摄像机操作包括滚轮缩放摄像机，WASD 移动摄像机等。
</details>



## 地图
<details>
  <summary>星系</summary>
  星系分为三个大类：资源星系，宜居星系与黑洞。宜居星系为资源星系的子集，在field中加入boolean值isLivable进行判断。资源星系只产出金属与能量，而宜居星系产出金属，能量与粮食。黑洞则只产出科技值。
</details>

<details>
  <summary>路径</summary>
  路径包含长度和速率的数据，长度代表两个星系间的距离，速率则是单位在此处移动的速度增幅或减幅，比如其他派系的单位在虚无的领地中通过路径的速率只有50%，所有单位在星穹列车行驶过后的路径上行驶则速率会达到200%。
</details>

## 数值
<details>
  <summary>资源数值</summary>
  资源数值包括了人口，粮食，金属，能量，科技值。其中人口增长通过额外粮食计算，非长生种的派系人口会周期性因寿命而减少。粮食通过宜居星球或科技树中的空间站，巡猎的仙舟等产出。金属与能量通过在星系上建立前哨站来获取。科技值的计算则是：每tick科技值=人口*科技速率，其中智识的额外能量越高则科技速率越高。
</details>
<details>
  <summary>单位数值</summary>
  单位数值：单位数值包括了生命值，攻击力，防御力，护盾值，移动速度。其中战斗状态下的伤害计算=攻击方攻击力-防御方防御力，若防御方存在护盾则优先消耗护盾值再消耗生命值。
</details>

## 基本派系行动
<details>
<summary>殖民</summary>
使用令使在星球上建立前哨站，使当前星球成为该派系的领地范围（所需tick数：？？）
</details>
<details>
<summary>战争</summary>
使用令使把当前星球上的工作人口转化为战争人口。
</details>
<details>
<summary>建造</summary>
使用人口在当前星球上建造设施，所需tick数与当前星球工作人口相关
</details>
<details>
<summary>TBD</summary>
TBD
</details>
<details>
<summary>TBD</summary>
TBD
</details>


## 派系特殊行动
<details>
  <summary>毁灭</summary>

  - 派系简介：
  毁灭派系的特征是攻击欲望强，且毁灭派系的人口不需要粮食维系，而是通过能量维系。毁灭的星神为纳努克，拥有七位毁灭令使。毁灭收集资源的方式不通过在资源星系上开采，而是直接破坏星球获取该星球的所有能源以及金属，因此毁灭的领地不需要通过修建前哨站来扩展，而是通过毁灭星系来扩展。只有令使以及星神拥有破坏星球的能力。被破坏后的星系将无法产出能源与金属。

  - 派系特性：
  1.毁灭派系只有战争人口
  2.毁灭令使占领一颗星球后可以是该星球的一部分人口转化为毁灭人口
  3.占领一颗星球后获得该星球的全部能量

  - 行动逻辑：
</details>

<details>
  <summary>存护</summary>

  - 派系简介：
  存护派系的攻击欲望很低，其星神为克里珀，拥有数十位存护令使。存护特有的建筑天慧星墙为克里伯的造物，其作用是使领地范围内的友军拥有护盾，且护盾被击碎时对地方造成护盾值等量得伤害。存护特色组织星际和平公司能够根据路径数量提供能源和金属的产出。存护派系的人口需要通过粮食来维系。

  - 派系特性：
  1.派系工作人口效率提高
  2.可以在星球之间修建路径获得额外资源加成
    
  - 行动逻辑：
</details>

<details>
  <summary>巡猎</summary>

  - 派系简介：
  巡猎在游戏初期并没有星神，只有九艘巨舰在银河中穿行，也没有领地。巡猎在初期时路过每个资源星系时都能获取其资源的一部分，人口通过九艘巨舰产出的粮食维系。在遇到丰饶星神之后巡猎的人口不再出现非战争死亡式减少，且出现星神岚以及七位巡猎令使。巡猎本身的攻击欲望较低，但当与其他派系发生战争之后巡猎对其的攻击欲望会迅速提高。在拥有星神之后巡猎派系能拥有领地范围。

  - 派系特性：
  1.工作人口可以升级巨舰获得更高资源产出与战斗功能
  2.获得星神前后享受不同buff

  - 行动逻辑：
</details>

<details>
  <summary>智识</summary>

  - 派系简介：
  智识的特点是科技发展速度极高，其攻击欲望并不强，扩展领地欲望也不强。智识的星神博士尊，其拥有八十四为智识的令使，也为天才俱乐部的成员。除此之外智识不存在额外人口，也不需要通过粮食进行人口维系，但博士尊的额外科技值加成需要能量维持。
  
  - 派系特性：
  1.令使负责建造以及战争行动
  2.令使可以创造机械生命加速行动速率
    
  - 行动逻辑：
</details>

<details>
  <summary>虚无</summary>

  - 派系简介：
  虚无没有人口，其星神IX为一个在星系上移动的黑洞，当IX移动到一个星系后会对星系进行持续一段时间的吞噬，吞噬过后会使这个星系中心留下一个特殊的黑洞，这也是虚无领地的标志。所有其他派系的单位在虚无领地中移动的速度降低50%，防御力降低50%。

  - 行动逻辑：
    开摆
</details>

<details>
  <summary>同谐</summary>

  - 派系简介：
  同谐的特点是同化的能力，当同谐领地与其他派系的领地接壤时，同谐便会开始对接壤部分的对方领地开始同化，如果对方不选择宣战，则在一定的时间之后同谐便会获得该领地。同谐的星神希佩，拥有十位令使级别的成员。同谐派系需要粮食来维系人口，但消耗的粮食数量下降。

  - 行动逻辑：
</details>

<details>
  <summary>丰饶</summary>

  - 派系简介：
  丰饶一开始并没有领地，丰饶的星神药师会在银河中游荡，当遇到拥有丰富资源的星系时药师会使这个星系从资源星系变成宜居星系，并且使其成为丰饶的领地。每一个被变化的星系都会诞生一名丰饶的令使和一定数量的人口。丰饶对高资源星系和宜居星系的占有欲强，且丰饶人口不会通过寿命减少，当丰饶人口消耗的粮食数量大于获得的粮食数量时会大幅提高发动战争的欲望。

  - 行动逻辑：
</details>

<details>
  <summary>开拓</summary>

  - 派系简介：
  开拓派系不存在星神，但是拥有两位令使。在地图生成时开拓会拥有星穹列车，星穹列车移动过的路径之后会变为特殊路径，其他所有单位在特殊路径上行驶速度提高100%。星穹列车会朝向拥有高资源且未被星穹列车探索过的星系行驶。开拓不存在人口，也不需要消耗粮食和能量进行维护。

  - 行动逻辑：
</details>
