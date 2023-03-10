# microservice
电商商城后端WebApi微服务：.NET6.0

#### 一、项目介绍
 + 1.1项目开发工具: Visual Studio 2022
 + 1.2.应用技术：
	+ 1.NET6.0 WebApi + mysql + Redis + ORM(EntityFrameworkCore) + RabbitMQ
	+ 2.AOP + Autofac + AutoMapper + JWT + log4net + Swagger 
	+ 3.Consul(微服务注册、服务健康检查、服务发现) + Polly(超时、重试、降级、熔断) + Nacos(服务配置中心)
	+ 4.DDD领域驱动设计模式微服务拆解
	+ 5.微服务模块：用户微服务、商品微服务、库存微服务、订单微服务
 + 1.3 代码git clone：https://github.com/xiehuihuang/microservice.git
 + 1.4 BaGet私服搭建和发布Nuget包并上传到私服： Infrastructure基础公共模块
	+ https://www.yuque.com/u22464947/bvpam9/yzr44dirk6914grc?singleDoc# 《6. 私服Nuget搭建》
 + 1.5 docker安装redis
	+ https://www.yuque.com/u22464947/loqoih/oxrltb?singleDoc# 《3. docker安装redis》
 + 1.6 docker安装mysql
	+ https://www.yuque.com/u22464947/loqoih/zxzu6a?singleDoc# 《8. docker安装mysql》
 + 1.7 docker安装consul与配置
	+ https://www.yuque.com/u22464947/loqoih/uxslql?singleDoc# 《5.docker安装consul与配置》
 + 1.8 docker安装nacos与配置
	+ https://www.yuque.com/u22464947/loqoih/usk99m?singleDoc# 《6.docker安装nacos与配置》
 
#### 二、基础设施
  2.1 Infrastructure的5个通用类库—各有不同的职责
  + 1.Framework.Common          最基础通用
  + 2.Framework.Core            框架核心，第三方组件
  + 3.Framework.WebCore         Web开发常用扩展
  + 4.Framework.WechatPayCore   微信支付相关
  
  2.2 Infrastructure基础公共模块发布Nuget包并上传到私服
	
  
 
#### 三、项目框架结构
  1. UserMicroservice      用户微服务
	 + UserMicro.DTOModel
	 + UserMicro.Interface
	 + UserMicro.Model
	 + UserMicro.Service
	 + UserMicro.WebApi
  2. BrandMicroservice     品牌微服务
	 + BrandMicro.DTOModel
	 + BrandMicro.Interface
	 + BrandMicro.Model
	 + BrandMicro.Service
	 + BrandMicro.WebApi
  3. AuthMicroservice      鉴权微服务
	 + AuthMicro.DTOModel
	 + AuthMicro.Interface
	 + AuthMicro.Model
	 + AuthMicro.Service
	 + AuthMicro.WebApi
  4. GoodsMicroservice     商品微服务
	 + GoodsMicro.DTOModel
	 + GoodsMicro.Interface
	 + GoodsMicro.Model
	 + GoodsMicro.Service
	 + GoodsMicro.WebApi
  5. InventoryMicroservice 库存微服务
	 + InventoryMicro.DTOModel
	 + InventoryMicro.Interface
	 + InventoryMicro.Model
	 + InventoryMicro.Service
	 + InventoryMicro.WebApi
  6. OrderMicroservice     订单微服务
	 + OrderMicro.DTOModel
	 + OrderMicro.Interface
	 + OrderMicro.Model
	 + OrderMicro.Service
	 + OrderMicro.WebApi
![4 基于分层的微服务](https://user-images.githubusercontent.com/32085450/216821061-7ba961a9-8536-4c53-97d8-9322e66efb38.png)
![私服](https://user-images.githubusercontent.com/32085450/216871776-db5a7b97-0e92-45fb-8f7b-5289cdb6e366.png)
![microservice代码框架目录](https://user-images.githubusercontent.com/32085450/216821035-8f022c9b-218c-4c26-94d3-5f5d346d9e28.png)

![用户微服务WebApi](https://user-images.githubusercontent.com/32085450/216821075-13d6cbc0-0307-49bb-b38e-2f1a7f4b76ee.png)

![consul微服务注册](https://user-images.githubusercontent.com/32085450/216821098-cfe01a75-3df9-4103-bc52-ec1e36d5d017.png)

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
