public class Element : Particle
{
    protected override void OnValidate()
    {
        base.OnValidate();
        
        particleType = ParticleType.Difficult;
    }
}