using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Entities.Models.CommonModels;
using static Mission.Entities.Models.MissionListDto;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        List<MissionListDto> GetMissionList();
        Task<bool> AddMission(Missions mission);

        Task<MissionRequestViewModel?> GetMissionById(int id);
     

      
        string DeleteMission(int id);
        List<DropDownResponseModel> GetMissionThemeList();
        string UpdateMission(AddMissionRequestModel model);
        List<DropDownResponseModel> GetMissionSkillList();
        Task<IList<Missions>> ClientSideMissionList();

        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);

        List<MissionApplicationDto> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
        public string DeleteMissionApplication(int id);
        //public  Task<bool> DeleteMissionApplication(int missionAppId);
    }
}
