namespace Todo.BusinessLogic.Services;

public static class UpdateService
{
    public static async Task<TEntity> ReplaceOlderByNewTEntityWithoutNullPropertiesAsync<TEntity>(this TEntity older, TEntity newTEntity)
    {
        var newTEntityProperties = newTEntity.GetType().GetProperties();

        var notNullProperties = newTEntityProperties.Where(x => x.GetValue(newTEntity) is not null);

        foreach (var property in notNullProperties)
        {
            var newValue = property.GetValue(newTEntity);
            
            var samePropertyFromOlder = older.GetType().GetProperty(property.Name);
            
            samePropertyFromOlder.SetValue(older, newValue);
        }

        return older;
    }
}