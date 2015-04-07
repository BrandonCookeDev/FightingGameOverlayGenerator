using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SmashOverlayGeneratorMk2;
using SmashOverlayGeneratorWebServiceLib.Objects;

namespace SmashOverlayGeneratorWebServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISOGControlService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        bool addConnection(Connection c);

        [OperationContract]
        bool removeConnection(Connection c);

        [OperationContract]
        void pingConnections();

        [OperationContract]
        int requestConnectionNumber();

        [OperationContract]
        bool updateScore(int connNum, string score1, string score2);

        [OperationContract]
        bool updateInfo(Connection c, TourneyData td);

        [OperationContract]
        TourneyData getRecentTourneyData(Connection c);

        [OperationContract]
        bool authenticate(String uName, String password);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
