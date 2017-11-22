﻿using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Trinity.Daemon;
using Trinity.DynamicCluster.Consensus;
using Trinity.Network;

namespace Trinity.ServiceFabric
{
    public class NameService : INameService
    {
        private BackgroundTask m_bgtask;
        private const int c_bgtaskInterval = 10000;

        public string Address => GraphEngineService.Instance.Address;

        public int Port => GraphEngineService.Instance.Port;

        public int HttpPort => GraphEngineService.Instance.HttpPort;

        public Guid InstanceId { get; private set; }

        public NameService()
        {
            m_bgtask = new BackgroundTask(ScanNodesProc, c_bgtaskInterval);
            InstanceId = new Guid(Enumerable.Concat(
                             GraphEngineService.Instance.NodeContext.NodeInstanceId.ToByteArray(),
                             Enumerable.Repeat<byte>(0x0, 16))
                            .Take(16).ToArray());
        }

        public TrinityErrorCode Start()
        {
            ServerInfo my_si = new ServerInfo(Address, Port, Global.MyAssemblyPath, TrinityConfig.LoggingLevel);
            BackgroundThread.AddBackgroundTask(m_bgtask);
            return TrinityErrorCode.E_SUCCESS;
        }

        public void Dispose()
        {
            BackgroundThread.RemoveBackgroundTask(m_bgtask);
        }

        private int ScanNodesProc()
        {
            return c_bgtaskInterval;
        }

        public event EventHandler<Tuple<Guid, ServerInfo>> NewServerInfoPublished = delegate { };
    }
}