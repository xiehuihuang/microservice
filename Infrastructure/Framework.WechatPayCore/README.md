配置：
```
通过WechatRegisterExtend.AddWechatPay完成映射注册
需要配置BasicConfig的3个Id

        private static BasicConfig _BasicConfig = null;
        private class BasicConfig
        {
            public string AppID { get; set; }
            public string MchID { get; set; }
            public string Key { get; set; }
        }
```



