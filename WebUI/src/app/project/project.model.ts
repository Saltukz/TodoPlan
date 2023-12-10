
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
