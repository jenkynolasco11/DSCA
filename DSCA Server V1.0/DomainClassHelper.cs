using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace PruebaDLL
{
    partial class DomainClassHelper
    {
        public static List<string> GetUsersFromDomain(string usersPath, string domain)
        {
            List<string> Users = new List<string>();
            string container = usersPath.Substring(usersPath.IndexOf("LDAP://") + 7);
            DirectoryEntry node = new DirectoryEntry(usersPath);
            //PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain, container);
            //UserPrincipal us = UserPrincipal.FindByIdentity(
            foreach (DirectoryEntry newChild in node.Children)
            {
                if (newChild.SchemaClassName.ToLower() == "user" && newChild.Name.IndexOf("Guest") == -1 && newChild.Name.IndexOf("krbtgt") == -1)
                {
                    Users.Add(newChild.Name.Substring(newChild.Name.IndexOf("CN=") + 3));
                }
            }
            return Users;
        }

        public static List<string> GetUsersByGroup(string Group, string domain, string OU)
        {
            List<string> users = new List<string>();

            string container = OU.Substring(OU.IndexOf("LDAP://") + 7);
            PrincipalContext p = new PrincipalContext(ContextType.Domain, domain, container);
            GroupPrincipal g = GroupPrincipal.FindByIdentity(p,Group);

            foreach (var user in g.GetMembers())
            {
                users.Add(user.Name);
            }
            
            return users;
        }

        public static List<string> GetGroupsOfPath(string domain, string OU)
        {
            string container = OU.Substring(OU.IndexOf("LDAP://") + 7);
            List<string> groups = new List<string>();
            PrincipalContext p = new PrincipalContext(ContextType.Domain, domain, container);
            GroupPrincipal g = new GroupPrincipal(p,"*");
            PrincipalSearcher searchForGroups = new PrincipalSearcher(g);

            foreach (var group in searchForGroups.FindAll())
            {
                groups.Add(group.SamAccountName);
            }

            return groups;
        }

        public static List<string> GetGroupsByUser(string User, string domain, string OU)
        {
            string container = OU.Substring(OU.IndexOf("LDAP://") + 7);
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);//, domain, container);
            List<string> groupsList = new List<string>();

            using (ctx)
            {
                Principal p = Principal.FindByIdentity(ctx, IdentityType.SamAccountName, User);

                if (p == null)
                    return null;
                
                using (p)
                {
                    var groups = p.GetGroups();
                    using (groups)
                    {
                        foreach (Principal group in groups)
                        {
                            groupsList.Add(group.SamAccountName);
                        }
                    }
                }
            }

            return groupsList;
        }

        public static List<string> GetGroupsFromDomain(string domain, string OU)
        {
            List<string> groups = new List<string>();
            string container = OU.Substring(OU.IndexOf("LDAP://") + 7);
            PrincipalContext p = new PrincipalContext(ContextType.Domain, domain, container);
            GroupPrincipal g = new GroupPrincipal(p,"*");//, "Administrator");
            PrincipalSearcher searchForGroups = new PrincipalSearcher(g);

            foreach (var group in searchForGroups.FindAll())
            {
                groups.Add(group.SamAccountName);
            }

            return groups;
        }

        public static List<object> GetUserInfo(string usersPath, string domain, string name)
        {
            string container = usersPath.Substring(usersPath.IndexOf("LDAP://") + 7);
            UserPrincipal user = null;
            PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain, container);
            try
            {
                user = UserPrincipal.FindByIdentity(pc,name);

            }catch(Exception e)
            {
            }
        
            List<object> items = new List<object> { user.Name, user.SamAccountName, user.DistinguishedName, user.LastLogon, user.DisplayName};
            return items;
        }

        public static void AddUserToGroup(string User, string UsersOU, string domain, string group, string groupOU)//, string GroupOU)
        {
            try
            {
                using(PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain, groupOU))
                {
                    GroupPrincipal gp = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, group);
                    UserPrincipal up = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain,domain, UsersOU), IdentityType.Name, User);
                    
                    if (gp == null)
                        throw new Exception("Group doesn't exist!");

                    gp.Members.Add(up);
                    gp.Save();
                    gp.Dispose();
                }                
            }

            catch (DirectoryServicesCOMException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void RemoveUserFromGroup(string User, string UsersOU, string domain, string group, string groupOU)//, string GroupOU)
        {
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain, groupOU))
                {
                    GroupPrincipal gp = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, group);
                    UserPrincipal up = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain, domain, UsersOU), IdentityType.Name, User);

                    if (gp == null)
                        throw new Exception("Group doesn't exist!");

                    gp.Members.Remove(up);
                    gp.Save();
                    gp.Dispose();
                }
            }

            catch (DirectoryServicesCOMException e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void CreateGroup(string OuPath, string GroupName, GroupScope groupScope, bool isSecurity, string domain, string Description)
        {
            if (!DirectoryEntry.Exists("LDAP://CN=" + GroupName + "," + OuPath))
            {
                try
                {
                    PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain, OuPath);
                    GroupPrincipal gp = new GroupPrincipal(pc, GroupName);
                    gp.GroupScope = groupScope;
                    gp.IsSecurityGroup = isSecurity;
                    gp.Description = Description;
                    gp.Save();                    
                }
                catch (Exception e)
                {
                }
            }

            else
            {
                throw new Exception("exists");
            }
        }

        public static void RemoveGroup(string OuPath, string GroupName)
        {
            if (DirectoryEntry.Exists("LDAP://CN=" + GroupName + "," + OuPath))
            {
                try
                {
                    DirectoryEntry entry = new DirectoryEntry("LDAP://" + OuPath);
                    DirectoryEntry group = new DirectoryEntry("LDAP://CN=" + GroupName + "," + OuPath);
                    entry.Children.Remove(group);
                    entry.CommitChanges();
                }
                catch (Exception e)
                {
                }
            }

            else
            {
                throw new Exception("Doesn't exists");
            }
        }

        public static void CreateOU(string OU)
        {
            if(!DirectoryEntry.Exists("LDAP://" + OU))
            {
                try
                {
                    DirectoryEntry ADentry = new DirectoryEntry();
                    ADentry.Children.Add(OU, "OrganizationalUnit");
                    ADentry.CommitChanges();
                }
                catch(Exception e)
                {
                    /*
                    throw new Exception("Existe 
                    */
                }
            }
        }
    }
}
