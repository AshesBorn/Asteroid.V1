using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    
    public ParticleSystem explosion;

    public float respawnTime = 3.0f;

    public float respawnInvulniblityTime = 3.0f;

    public int lives = 3;

    public Text textScore;

    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid){

        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.75f) {
            this.score += 100;
        } else if (asteroid.size < 1.2f) {
            this.score += 50; 
        } else {
            this.score += 25;
        }

        textScore.text = score.ToString();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }

    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Default");
        this.player.gameObject.SetActive(true);
        this.player.Invoke(nameof(TurnOnCollisions), this.respawnInvulniblityTime);
    }

    private void TurnOnCollisions() {

        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;
    }
}
