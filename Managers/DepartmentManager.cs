//using EduReg.Common;
//using EduReg.Models.Dto;
//using EduReg.Services.Interfaces;

//namespace EduReg.Managers
//{
//    public class DepartmentManager
//    {
//        private readonly IDepartments _departmentRepository;

//        public DepartmentManager(IDepartments departmentRepository)
//        {
//            _departmentRepository = departmentRepository;
//        }

//        public async Task<GeneralResponse> CreateDepartmentAsync(DepartmentsDto model)
//        {
//            return await _departmentRepository.CreateDepartmentAsync(model);
//        }

//        public async Task<GeneralResponse> GetAllDepartmentsAsync()
//        {
//            return await _departmentRepository.GetAllDepartmentsAsync();
//        }

//        public async Task<GeneralResponse> GetDepartmentByIdAsync(int id)
//        {
//            return await _departmentRepository.GetDepartmentByIdAsync(id);
//        }

//        public async Task<GeneralResponse> GetDepartmentByNameAsync(string name)
//        {
//            return await _departmentRepository.GetDepartmentByNameAsync(name);
//        }

//        public async Task<GeneralResponse> UpdateDepartmentAsync(int id, DepartmentsDto model)
//        {
//            return await _departmentRepository.UpdateDepartmentAsync(id, model);
//        }

//        public async Task<GeneralResponse> DeleteDepartmentAsync(int id)
//        {
//            return await _departmentRepository.DeleteDepartmentAsync(id);
//        }
//    }
//}
    

