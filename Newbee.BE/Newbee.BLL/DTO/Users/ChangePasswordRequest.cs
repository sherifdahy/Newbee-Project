﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbee.BLL.DTO.Users;

public record ChangePasswordRequest
(
    string CurrentPassword,
    string NewPassword
);
