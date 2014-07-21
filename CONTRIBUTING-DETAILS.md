# Contributing Details

**Collaborating using Fork and Pull**

Forking is at the core of social coding at GitHub. To contribute, use the "Fork and Pull" model where you don't need to be granted access to a particular [blackbaud-community](https://github.com/blackbaud-community) project (repo), such as [Blackbaud-CRM-Conferences](https://github.com/blackbaud-community/Blackbaud-CRM-Conferences).  You can Fork (create your own personal copy) of a specific project and work independently with your personal copy of the code.  For the remainder of this material, the words 'project' and 'repo' and 'repository' are used interchangeably.  

See [Using Pull Requests and Collaborative Development Models](https://help.github.com/articles/using-pull-requests#a-quick-note-on-collaborative-development-models). 

**Assumptions**

This content assumes you have a basic understanding of Git and GitHub. You should of already have a grasp of the concepts related to a Repository, Fork, Clone, Branch, Commit, Push, and Pull Requests.  You should have some experience creating repositories, as well as forking, cloning, etc.  See **Resources** at the bottom of this content to get familiar with Git and GitHub.  

## Getting Started

Below is an outline of the high level steps required to grab the code, begin making changes, and optionally contribute your work back to the community.  Additional detail is provided in the following section named **Making Changes**.  

* Make sure you have a [GitHub account](https://github.com/signup/free)
* Fork - Fork the appropriate [blackbaud-community](https://github.com/blackbaud-community) repository using  GitHub.com.  Now you've got your own version to play with. 
* Clone - Clone your fork to your local computer.  Now the code for your fork is on your machine.
* Branch - Get off the master branch by setting up a development branch. 
* Hack - Hack away!  Begin making local changes to the code within your new development branch.  
* Commit - Commit your local changes.  (things are still local at this point)
* Push - Push your local changes up to your fork on GitHub.com
* Sign - ***Sign the CLA before you Push***
* Pull Request - Submit a Pull Request

Note:  Throughout this document, I will display images from the [GitHub for Windows](http://windows.github.com/) client Version 1.2.11.0.  The client helps you to become familiar with Git's main features by making cloning, branching, and syncing (push) relatively easy.  The Windows client is not a substitution for knowing why you should clone, branch, and push.  For that you should study and practice using the resources listed in the **Read Me** as well as read this content. 

## Making Changes

**Fork**

Creating a Fork is producing a personal copy of one our blackbaud-community projects/repos, such as [Blackbaud-CRM-BBDW](https://github.com/blackbaud-community/Blackbaud-CRM-BBDW).  Forking is the first step in contributing to a  repo.  It also allows you to use our source code as the starting point for your own projects. Forks act as a sort of bridge between the original Blackbaud-CRM repository and your personal copy. After you have forked a blackbaud-community repo/project, you will have a personal copy for your personal GitHub account.  If you plan on offering changes to the original project/repo, you can submit Pull Requests to help make a projects better by offering your changes up to the original project. Forking is at the core of social coding at GitHub.

You fork a project using GitHub.com. Navigate to the appropriate project, like the [Blackbaud CRM API Cookbook](https://github.com/blackbaud-community/Blackbaud-CRM-API-Cookbook) repo on GitHub.com and select the 'Fork' button.

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/ForkGitHubdotCom.png)

For more more information and a tutorial see [Fork a Repo](https://help.github.com/articles/fork-a-repo) which is part of the GitHub [bootcamp](https://help.github.com/categories/54/articles) and [Forking Projects](http://guides.github.com/overviews/forking/).


**Clone**

After you Fork, the nest step is to Clone your personal copy to your computer. Cloning will copy the files from GitHub.com to your local computer.  

If you want to copy a repository from the GitHub.com website, you can select the 'Clone in Desktop' button.

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/CloneGitHubdotCom.png)

**Branch**

At this point you have forked the code which created your own copy of the code.  You have also cloned the fork to your local machine.  If you plan on submitting improvements to the code samples and you want to contribute your codes changes to the community, you will need to create a new branch on your local machine.  *Each time that you want to commit a bug or a feature, you need to create a branch for it, which will be a copy of your master branch.*  Contributing means ultimately submitting a Pull Request.  *You can only have one Pull Request per branch.*  So it makes sense to create a new branch for each new feature or improvement you plan to make.  

To branch using GitHub for Windows, look for the 'switch branches' button.  Look for the name of your current branch which is shown next to the 'sync' button.  Clicking this 'switch branches' button displays all the existing branches and you will see a text box labeled 'Filter or Create New' for creating a new branch.  In the image, below I have already created a new branch named 'EnhanceSample' on my local machine.   

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/NewLocalBranchGitHubWindows.png)

For all you command-line junkies, check out [Create a new branch with git and manage branches](https://github.com/Kunena/Kunena-Forum/wiki/Create-a-new-branch-with-git-and-manage-branches) to see the low level commands using the command-line tool 'git'.   

**Hack**

Once you have a new branch established, you can edit your changes.  The first step is to know where your local files reside for the branch.  Using GitHub for Windows, you can open the Windows explorer to your working directory by selecting the 'tools and options' button in the upper right hand corner followed by clicking 'Open in explorer'.

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/OpenInExplorerGitHubWindows.png)

You can then edit the files locally on your computer using whatever editor makes sense. On your local computer git will track the changes in this working directory. After you have made and *TESTED* your local changes, you can provide a detailed summary of what you changed and then Commit.  

**Commit**

When you commit you are essentially taking a local snapshot of the changes on your *local* copy.  You can change more files locally and commit again.  At this point everything is local to your computer.    

The GitHub for Windows client will detect the changes to your code within your working directory. It will notify you when you have Uncommitted changes.    

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/UncommittedChangesGitHubWindows.png)

By selecting the 'show' button, you can provide a summary and description of the changes you made to the code.  After you have commented your changes, press the Commit button.

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/UncommittedChangesGitHubWindows2.png)

When you are ready to sync your local changes back up to GitHub.com you will need to perform a 'Push'. 

**Sign the CLA Before you Push**

Your efforts will likely require you to sign a Contributor License Agreement (CLA).  We recommend you talk to your employer (if applicable) before signing any sort of CLA, since some employment agreements may have restrictions on your contributions to other projects.  *So, whether it's a trivial enhancement or a major addition/improvement, you will need to sign the CLA before you push.* For questions about your potential contributions, you can contact trip.ottinger@blackbaud.com.  Once we have the necessary legal documents and process in place, we will provide a link to the CLA and/or other legal documentation.  

**Push**

Pushing will synchronize the files from your local computer to your personal copy (fork) of the repo within GitHub.com.  

**Before you push changes to your fork of the repository, you should sign the Contributor License Agreement.  Sign the CLA Before you Push.**  

If you are using the GitHub for Windows client, you can use the 'Sync' button to push the commits from your branch up to GitHub.com. Sync can also be used to bring down changes from the branch.  

The direction of the arrow along with the number indicate the number of commits that your local branch is either ahead or behind.  In the image below, the up arrow and the number '1' indicates we have a single commit that needs to be sync'd up to GitHub.com.  In other words, we are locally ahead of GitHub.com by one commit.  

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/SyncAhead1GitHubWindows.png)

If you want to propose your changes to be merged back into Blackbaud's repo, there is still one more step: a Pull Request.

**Pull Request**

You may find yourself wanting to contribute to one of our blackbaud-community projects, whether to add features or to fix bugs. After making changes, you will want to let the original author know about them by sending a Pull Request. A Pull Request is a request to pull (merge) your code back into Blackbaud's original repository's master branch. Pull requests let you tell others about changes you've pushed to your GitHub.com repository. Once a pull request is sent, Blackbaud can review the set of changes, discuss potential modifications, and even push follow-up commits if necessary.  

Pull Requests open the discussion.  It may take sometime to review your changes.  You may receive a request for additional explanation.  A Pull Request has to be approved by the project maintainer(s) here at Blackbaud.  Your changes may be rejected and in which case your changes will not be merged upstream into the repo.  But, your copy will remain on GitHub and it may inspire and spark the imagination of someone else within the community.   

Within GitHub.com, navigate to *your* repository that contains the commits you wish to merge. Then press the 'Compare & pull request' button.   

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/CompareandPull.png)

You will need to provide lots of details within the Pull Request explaining why your change is a good thing for everyone else in the community.  When you are ready click the 'Send pull request' button.    

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/PullRequest.png)

From Blackbaud's perspective within GitHub.com, after you submit the Pull Request, Blackbaud will be able to view the request and compare the changes against the our master branch.  

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/BBViewPullRequest.png)

Blackbaud will either accept the pull request and merge your changes into the project's master branch, comment and discuss any issues with the branch, or decide not to merger your changes. 

![](http://blackbaud-community.github.io/Blackbaud-CRM/images/ReviewMergePullRequest.png)


For more information see [Using Pull Requests](https://help.github.com/articles/using-pull-requests)

## Resources ##

- [Contributing Basics](CONTRIBUTING-BASICS.md)
- [GitHub](https://github.com/)
- [General GitHub documentation](http://help.github.com/)
- [GitHub pull request documentation](http://help.github.com/send-pull-requests/)
- [Pro Git Book](http://git-scm.com/book), Scott Chacon (Free on-line)
- [How to GitHub: Fork, Branch, Track, Squash and Pull Request](https://gun.io/blog/how-to-github-fork-branch-and-pull-request/)
- [Other good resources](https://help.github.com/articles/what-are-other-good-resources-for-learning-git-and-github)
- [Git Home](http://git-scm.com/)
- [Getting Started](http://git-scm.com/book/en/Getting-Started-Git-Basics)
- [Documentation](http://git-scm.com/documentation)
- [Contributor License Agreement]
