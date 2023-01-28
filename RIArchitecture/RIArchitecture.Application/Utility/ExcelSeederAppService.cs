using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RIArchitecture.Application.Contracts.Administration.Interfaces;
using RIArchitecture.Application.Contracts.Utility;
using RIArchitecture.Core.RIArchitectureCoreBase.Interface;

namespace RIArchitecture.Application.Utility
{
    public class ExcelSeederAppService : RIArchitectureAppService//, IExcelSeederAppService
    {
        private readonly IFileUploadAppService _fileUploadAppService;
        private readonly IUserAppService _userAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExcelSeederAppService> _logger;

        public ExcelSeederAppService(
            IFileUploadAppService fileUploadAppService,
            IUserAppService userAppService,
            IRoleAppService roleAppService,
            IWebHostEnvironment env,
            IUnitOfWork unitOfWork,
            ILogger<ExcelSeederAppService> logger)
        {
            _fileUploadAppService = fileUploadAppService;
            _userAppService = userAppService;
            _roleAppService = roleAppService;
            _env = env;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //public async Task<int> GetAllAreaDataFromExcelAsync(FileUploadDto fileUpload)
        //{
        //    try
        //    {
        //        var areaData = await UploadAndReadExcelFileAsync<AreaDto>(fileUpload);

        //        foreach (var item in areaData)
        //        {
        //            item.STATE = item.STATE.ToUpper().Trim();
        //            item.ZONE = item.ZONE.ToUpper().Trim();
        //            item.REGION = item.REGION.ToUpper().Trim();
        //            item.BRANCH = item.BRANCH.ToUpper().Trim();
        //            item.TERRITORY = item.TERRITORY.ToUpper().Trim();
        //            item.DISTRICT = item.DISTRICT.ToUpper().Trim();
        //        }

        //        areaData = areaData.Select(x =>
        //        {
        //            x.REGION = !string.IsNullOrEmpty(x.REGION) ? x.REGION.ToUpper() : null;
        //            return x;
        //        }).ToList();

        //        List<Zone> zones = new List<Zone>();
        //        List<State> states = new List<State>();
        //        List<Region> regions = new List<Region>();
        //        List<Branch> branches = new List<Branch>();
        //        List<Territory> territories = new List<Territory>();
        //        List<District> districts = new List<District>();

        //        #region Inserting zones
        //        var listOfZones = areaData.Select(x => x.ZONE).Distinct().ToList();

        //        if (listOfZones.Count == 0)
        //            _logger.LogError("Zone data not found to insert");

        //        foreach (var zone in listOfZones)
        //        {
        //            var insertedZone = _unitOfWork.Repository<Zone>().Insert(new Zone
        //            {
        //                Name = zone,
        //                Source = "SEED"
        //            });
        //            zones.Add(insertedZone);
        //        }
        //        var zoneOperations = await _unitOfWork.Complete();
        //        #endregion

        //        #region Inserting states
        //        if (zones.Count == 0)
        //            _logger.LogError("State data not found to insert");
        //        else
        //        {
        //            var listOfZoneAndState = areaData.Select(x => new { x.ZONE, x.STATE }).Distinct()
        //                                    .GroupBy(elem => elem.STATE).Select(group => group.First())
        //                                    .ToList();


        //            foreach (var item in listOfZoneAndState)
        //            {
        //                var zoneId = zones.Where(x => x.Name == item.ZONE).Select(x => x.Id).FirstOrDefault();
        //                var insertedState = _unitOfWork.Repository<State>().Insert(new State
        //                {
        //                    ZoneId = zoneId,
        //                    Name = item.STATE,
        //                    Source = "SEED"
        //                });
        //                states.Add(insertedState);
        //            }
        //        }
        //        var stateOperations = await _unitOfWork.Complete();
        //        #endregion

        //        #region Inserting Regions
        //        if (zones.Count == 0)
        //            _logger.LogError("Region data not found to insert");
        //        else
        //        {
        //            var listOfZoneAndRegion = areaData.Select(x => new { x.ZONE, x.REGION }).Distinct()
        //                                        .GroupBy(elem => elem.REGION).Select(group => group.First())
        //                                        .ToList();


        //            foreach (var item in listOfZoneAndRegion)
        //            {
        //                var zoneId = zones.Where(x => x.Name == item.ZONE).Select(x => x.Id).FirstOrDefault();
        //                var insertedRegion = _unitOfWork.Repository<Region>().Insert(new Region
        //                {
        //                    ZoneId = zoneId,
        //                    Name = item.REGION,
        //                    Code = item.REGION,
        //                    Source = "SEED"
        //                });
        //                regions.Add(insertedRegion);
        //            }
        //        }
        //        var regionOperations = await _unitOfWork.Complete();
        //        #endregion

        //        #region Inserting Branches
        //        if (regions.Count == 0)
        //            _logger.LogError("Branches data not found to insert");
        //        else
        //        {
        //            var listOfRegionAndBranches = areaData.Select(x => new { x.BRANCH, x.REGION }).Distinct()
        //                                        .GroupBy(elem => elem.BRANCH).Select(group => group.First())
        //                                        .ToList();


        //            foreach (var item in listOfRegionAndBranches)
        //            {
        //                var regionId = regions.Where(x => x.Name == item.REGION).Select(x => x.Id).FirstOrDefault();
        //                var insertedRegion = _unitOfWork.Repository<Branch>().Insert(new Branch
        //                {
        //                    RegionId = regionId,
        //                    Name = item.BRANCH,
        //                    Source = "SEED"
        //                });
        //                branches.Add(insertedRegion);
        //            }
        //        }
        //        var branchesOperations = await _unitOfWork.Complete();
        //        #endregion

        //        #region Inserting Territories
        //        if (branches.Count == 0)
        //            _logger.LogError("Territories data not found to insert");
        //        else
        //        {
        //            var listOfBranchAndTerritory = areaData.Select(x => new { x.BRANCH, x.TERRITORY }).Distinct()
        //                                        .GroupBy(elem => elem.TERRITORY).Select(group => group.First())
        //                                        .ToList();


        //            foreach (var item in listOfBranchAndTerritory)
        //            {
        //                var branchId = branches.Where(x => x.Name == item.BRANCH).Select(x => x.Id).FirstOrDefault();
        //                var insertedRegion = _unitOfWork.Repository<Territory>().Insert(new Territory
        //                {
        //                    BranchId = branchId,
        //                    Name = item.TERRITORY,
        //                    Source = "SEED"
        //                });
        //                territories.Add(insertedRegion);
        //            }
        //        }
        //        var territoriesOperations = await _unitOfWork.Complete();
        //        #endregion

        //        #region Inserting Districts
        //        if (territories.Count == 0)
        //            _logger.LogError("Districts data not found to insert");
        //        else
        //        {
        //            var listOfTerritoryAndDistrict = areaData.Select(x => new { x.DISTRICT, x.TERRITORY }).Distinct()
        //                                        .GroupBy(elem => elem.DISTRICT).Select(group => group.First())
        //                                        .ToList();


        //            foreach (var item in listOfTerritoryAndDistrict)
        //            {
        //                var territoryId = territories.Where(x => x.Name == item.TERRITORY).Select(x => x.Id).FirstOrDefault();
        //                var insertedRegion = _unitOfWork.Repository<District>().Insert(new District
        //                {
        //                    TerritoryId = territoryId,
        //                    Name = item.DISTRICT,
        //                    Source = "SEED"
        //                });
        //                districts.Add(insertedRegion);
        //            }
        //        }
        //        var districtOperations = await _unitOfWork.Complete();
        //        #endregion

        //        var operations = zoneOperations + stateOperations + regionOperations + branchesOperations + territoriesOperations + districtOperations;

        //        return operations;
        //    }
        //    catch (Exception ex)
        //    {
        //        var rootPath = _env.WebRootPath;
        //        string path = $@"{rootPath}\{fileUpload.FileUploadFolder.ToString()}\";
        //        var absoluteFilePath = path + fileUpload.File.FileName;
        //        if (File.Exists(absoluteFilePath))
        //        {
        //            File.Delete(absoluteFilePath);
        //            _logger.LogInformation($"{fileUpload.File.FileName} file deleted from folder {path}.");
        //        }
        //        _logger.LogError(ex, ex.Message);
        //        return 0;
        //    }
        //}

        //public async Task<List<StakeHoldersExcelUploadOutputDto>> GetAllStakeHolderDataFromExcelAsync(FileUploadDto fileUpload)
        //{
        //    var failedToInsert = new List<StakeHoldersExcelUploadOutputDto>();
        //    var stakeHolderDetails = await UploadAndReadExcelFileAsync<StakeHoldersDto>(fileUpload);

        //    foreach (var item in stakeHolderDetails)
        //    {
        //        item.Region = item.Region.ToUpper().Trim();
        //        item.Zone = item.Zone.ToUpper().Trim();
        //        item.Branch = item.Branch.ToUpper().Trim();
        //        item.Territory = item.Territory.ToUpper().Trim();
        //        item.EmployeeCode = item.EmployeeCode.ToUpper().Trim();
        //        item.Name = item.Name.Trim();
        //        item.Email = item.Email.Trim();
        //        item.PhoneNumber = item.PhoneNumber.ToUpper().Trim();
        //        item.Role = item.Role.ToUpper().Trim();
        //    }

        //    var areaDetails = await _areaAppService.AreaDetailListAsync();
        //    foreach (var item in stakeHolderDetails)
        //    {
        //        try
        //        {
        //            item.Role = item.Role.RemoveAllWhitespace();
        //            if (await _roleAppService.RoleExistAsync(item.Role))
        //            {
        //                var roleTypeAndArea = GetRoleTypeAndArea(item);
        //                if (roleTypeAndArea.Item1 != RoleType.None)
        //                {
        //                    var areaDetail = _areaAppService.GetAreaDetailByRole(new AreaDetailByRoleInputDto
        //                    {
        //                        Role = roleTypeAndArea.Item1,
        //                        AreaDetails = areaDetails,
        //                        Area = roleTypeAndArea.Item2
        //                    });

        //                    if (areaDetail == null)
        //                    {
        //                        _logger.LogError($"{roleTypeAndArea.Item2} area not found");
        //                        var returnObject = ObjectMapper.Map<StakeHoldersDto, StakeHoldersExcelUploadOutputDto>(item);
        //                        returnObject.Remark = $"{roleTypeAndArea.Item2} area not found";
        //                        failedToInsert.Add(returnObject);
        //                    }
        //                    else
        //                    {
        //                        var userDto = ObjectMapper.Map<AreaDetailWithIdDto, UserDto>(areaDetail);

        //                        if (userDto.ZoneId == 0 || userDto.TerritoryId == 0 ||
        //                            userDto.BranchId == 0 || userDto.DistrictId == 0 ||
        //                            userDto.StateId == 0 || userDto.RegionId == 0)
        //                        {
        //                            var returnObject = ObjectMapper.Map<StakeHoldersDto, StakeHoldersExcelUploadOutputDto>(item);
        //                            returnObject.Remark = $"{roleTypeAndArea.Item2} not found in system";
        //                            failedToInsert.Add(returnObject);
        //                            _logger.LogError($"{roleTypeAndArea.Item2} not found in system");
        //                        }
        //                        else
        //                        {
        //                            userDto.Roles = new List<string>() { item.Role };
        //                            userDto.Password = "Password@123";
        //                            userDto.PhoneNumber = item.PhoneNumber;
        //                            userDto.UserName = string.IsNullOrEmpty(item.Name) ? null : item.Name.RemoveAllWhitespace();
        //                            userDto.Email = item.Email;
        //                            userDto.PhoneNumber = item.PhoneNumber;
        //                            userDto.EmployeeCode = item.EmployeeCode;
        //                            userDto.Name = item.Name;
        //                            var createdUser = await _userAppService.CreateAsync(userDto);

        //                            if (createdUser == Guid.Empty)
        //                            {
        //                                var returnObject = ObjectMapper.Map<StakeHoldersDto, StakeHoldersExcelUploadOutputDto>(item);
        //                                returnObject.Remark = $"{item.Name} User not created";
        //                                failedToInsert.Add(returnObject);
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    var returnObject = ObjectMapper.Map<StakeHoldersDto, StakeHoldersExcelUploadOutputDto>(item);
        //                    returnObject.Remark = $"Role type {item.Role} not found in RoleType enum";
        //                    failedToInsert.Add(returnObject);
        //                    _logger.LogError($"Role type {item.Role} not found in RoleType enum");
        //                }
        //            }
        //            else
        //            {
        //                var returnObject = ObjectMapper.Map<StakeHoldersDto, StakeHoldersExcelUploadOutputDto>(item);
        //                returnObject.Remark = $"Role type {item.Role} not found in system";
        //                failedToInsert.Add(returnObject);
        //                _logger.LogError($"Role type {item.Role} not found in system");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            var returnObject = ObjectMapper.Map<StakeHoldersDto, StakeHoldersExcelUploadOutputDto>(item);
        //            returnObject.Remark = $"Error message : {ex.Message}";
        //            failedToInsert.Add(returnObject);
        //            _logger.LogError(ex, $"Error message : {ex.Message}");
        //        }
        //    }

        //    Console.WriteLine(JsonConvert.SerializeObject((failedToInsert.Select(x => x.Territory).ToList()),Formatting.Indented));

        //    return failedToInsert;
        //}

        //private async Task<List<T>> UploadAndReadExcelFileAsync<T>(FileUploadDto fileUpload)
        //{
        //    var fileUploadOutput = await _fileUploadAppService.FileUploadAsync(fileUpload);
        //    List<T> list = new List<T>();
        //    if (fileUploadOutput.IsSuccess)
        //    {
        //        Type typeOfObject = typeof(T);
        //        var fileRelativePath = fileUploadOutput.Path.Replace(@"\\", @"\");
        //        var path = $@"{_env.WebRootPath}{fileRelativePath}";
        //        using (IXLWorkbook workbook = new XLWorkbook(path))
        //        {
        //            var worksheet = workbook.Worksheets.First();
        //            var properties = typeOfObject.GetProperties();
        //            var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });

        //            foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
        //            {
        //                T obj = (T)Activator.CreateInstance(typeOfObject);

        //                foreach (var prop in properties)
        //                {
        //                    int colIndex = columns.SingleOrDefault(x => x.Value.ToString() == prop.Name.ToString()).Index;
        //                    var val = row.Cell(colIndex).Value;
        //                    var type = prop.PropertyType;
        //                    prop.SetValue(obj, Convert.ChangeType(val, type));
        //                }

        //                list.Add(obj);
        //            }
        //        }
        //    }

        //    return list;
        //}

        //public static RoleType GetRoleTypeEnum(string role)
        //{
        //    RoleType roleType = RoleType.None;
        //    Enum.TryParse(role, out roleType);
        //    return roleType;
        //}

        //public static Tuple<RoleType, string> GetRoleTypeAndArea(StakeHoldersDto input)
        //{
        //    RoleType roleType = RoleType.None;
        //    Enum.TryParse(input.Role, out roleType);
        //    Tuple<RoleType, string> returnValue = new Tuple<RoleType, string>(roleType, null);
        //    switch (roleType)
        //    {
        //        case RoleType.BM:
        //            returnValue = new Tuple<RoleType, string>(roleType, input.Branch);
        //            break;
        //        case RoleType.BTM:
        //            goto case RoleType.BM;
        //        case RoleType.RM:
        //            returnValue = new Tuple<RoleType, string>(roleType, input.Region);
        //            break;
        //        case RoleType.RTM:
        //            goto case RoleType.RM;
        //        case RoleType.TSE:
        //            returnValue = new Tuple<RoleType, string>(roleType, input.Territory);
        //            break;
        //        case RoleType.TTE:
        //            goto case RoleType.TSE;
        //        case RoleType.ZH:
        //            returnValue = new Tuple<RoleType, string>(roleType, input.Zone);
        //            break;
        //    }

        //    return returnValue;
        //}
    }
}
