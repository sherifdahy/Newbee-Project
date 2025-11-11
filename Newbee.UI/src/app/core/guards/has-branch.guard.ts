import { inject, Inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { BranchService } from '../services/branch.service';

export const hasBranchGuard: CanActivateFn = (route, state) => {
  const branchService = inject(BranchService);
  const router = inject(Router);

  if(branchService.hasBranch())
    return true;

  router.navigateByUrl('branch-selection');
  return false;
};
