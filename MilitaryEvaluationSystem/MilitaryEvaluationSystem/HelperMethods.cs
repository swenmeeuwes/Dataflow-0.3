using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryEvaluationSystem {
    static class HelperMethods {

        public static DateTime ConvertUnitToDate(int unixTimeStamp) {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static float Remap(float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float CalculateStressLevel(int heartBeat, float bodyTemp) {
            float mappedHeartRate = Remap(heartBeat, 50, 260, 1, 10);
            float mappedBodyTemp = Remap(bodyTemp, 22, 27, 1, 10);

            return (mappedHeartRate * 0.7f + mappedBodyTemp * 0.3f);    
        }
    }
}
