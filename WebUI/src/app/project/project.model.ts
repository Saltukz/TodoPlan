
export class ProjectModel{
  ProjectName:string;
  ProviderUrl:string;
  Developers: Developer[]
  Tasks : Task[]
}

export class Developer{
  Name:string;
  Capacity:number;
}

export class Task{
  Name:string;
  Duration:number;
  Difficulty:number;
}
export interface ResponseProjectGet {
  name: string | null;
  time: number | null;
  lstDeveloper: DeveloperVM[] | null;
}

export interface DeveloperVM {
  IdDeveloper: number;
  IdProject: number;
  Name: string;
  Capacity: number;
  IsGotTask: boolean;
}
