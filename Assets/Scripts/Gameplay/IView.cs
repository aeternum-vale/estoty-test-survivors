namespace Gameplay
{
	public interface IView
	{
		void SetAmmoAmount(int ammo);
		void SetKillsCount(int killsCount);
		void SetLevel(int level);
		void SetNormalizedExperience(float normalizedExperience);
		void SetNormalizedHealth(float healthPercentage);
	}
}