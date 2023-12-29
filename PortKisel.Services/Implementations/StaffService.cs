using AutoMapper;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Enums;
using PortKisel.Context.Contracts.Models;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Models;
using PortKisel.Services.Contracts.ModelsRequest;

namespace PortKisel.Services.Implementations
{
    public class StaffService : IStaffService, IServiceAnchor
    {
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStaffWriteRepository staffWriteRepository;

        public StaffService(IStaffReadRepository staffReadRepository, IMapper mapper, IStaffWriteRepository staffWriteRepository, IUnitOfWork unitOfWork)
        {
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.staffWriteRepository = staffWriteRepository;
        }


        async Task<IEnumerable<StaffModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var staffs = await staffReadRepository.GetAllAsync(cancellationToken);

            var liststaffModel = new List<StaffModel>();
            foreach(var staff in staffs)
            {
                var staffMap = mapper.Map<StaffModel>(staff);
                liststaffModel.Add(staffMap);
            }
            return liststaffModel;
        }

        async Task<StaffModel?> IStaffService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var staff = mapper.Map<StaffModel>(item);
            return staff;
        }


        async Task<StaffModel> IStaffService.AddAsync(StaffRequestModel staff, CancellationToken cancellationToken)
        {
            var item = new Staff
            {
                Id = Guid.NewGuid(),
                FIO = staff.FIO,
                Post = (Posts)staff.Post
            };

            staffWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<StaffModel>(item);
        }

        async Task<StaffModel> IStaffService.UpdateAsync(StaffRequestModel source, CancellationToken cancellationToken)
        {
            var targetStaff = await staffReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetStaff == null)
            {
                throw new PortEntityNotFoundException<Staff>(source.Id);
            }
            targetStaff.FIO = source.FIO;
            targetStaff.Post = (Posts)source.Post;

            staffWriteRepository.Update(targetStaff);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<StaffModel>(targetStaff);
        }

        async Task IStaffService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetStaff = await staffReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetStaff == null)
            {
                throw new PortEntityNotFoundException<Staff>(id);
            }
            if (targetStaff.DeletedAt.HasValue)
            {
                throw new PortInvalidOperationException($"Документ с идентификатором {id} уже удален");
            }

            staffWriteRepository.Delete(targetStaff);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
