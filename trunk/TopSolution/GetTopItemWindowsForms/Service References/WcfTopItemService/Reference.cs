﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.296
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GetTopItemWindowsForms.WcfTopItemService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfTopItemService.ITopItemService")]
    public interface ITopItemService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITopItemService/SaveTopItemList", ReplyAction="http://tempuri.org/ITopItemService/SaveTopItemListResponse")]
        void SaveTopItemList(TopEntity.TopItem[] items);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITopItemService/DeleteTopItems", ReplyAction="http://tempuri.org/ITopItemService/DeleteTopItemsResponse")]
        void DeleteTopItems(string keyword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITopItemService/GetAllKeywords", ReplyAction="http://tempuri.org/ITopItemService/GetAllKeywordsResponse")]
        TopEntity.TopKeywords[] GetAllKeywords();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITopItemServiceChannel : GetTopItemWindowsForms.WcfTopItemService.ITopItemService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TopItemServiceClient : System.ServiceModel.ClientBase<GetTopItemWindowsForms.WcfTopItemService.ITopItemService>, GetTopItemWindowsForms.WcfTopItemService.ITopItemService {
        
        public TopItemServiceClient() {
        }
        
        public TopItemServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TopItemServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TopItemServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TopItemServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SaveTopItemList(TopEntity.TopItem[] items) {
            base.Channel.SaveTopItemList(items);
        }
        
        public void DeleteTopItems(string keyword) {
            base.Channel.DeleteTopItems(keyword);
        }
        
        public TopEntity.TopKeywords[] GetAllKeywords() {
            return base.Channel.GetAllKeywords();
        }
    }
}
